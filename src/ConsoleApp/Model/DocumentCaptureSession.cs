using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace ConsoleApp.Model
{
    internal class DocumentCaptureSession : CaptureSession
    {
        public static DocumentCaptureSession Create(int pageLimit)
        {
            return new DocumentCaptureSession(Guid.NewGuid(), pageLimit, CaptureSessionState.Created, null);
        }

        private readonly StateMachine<CaptureSessionState, DocumentCaptureSessionCommand> machine;
        private readonly int pageLimit;
        private readonly IList<Page> pages;
        private readonly StateMachine<CaptureSessionState, DocumentCaptureSessionCommand>.TriggerWithParameters<ProcessRequest> processRequestTrigger;

        public DocumentCaptureSession(Guid id, int pageLimit, CaptureSessionState state, IList<Page> pages) : base(id, state)
        {
            this.pageLimit = pageLimit;
            this.pages = pages ?? new List<Page>();
            machine = new StateMachine<CaptureSessionState, DocumentCaptureSessionCommand>(() => State, s => State = s);

            processRequestTrigger = machine.SetTriggerParameters<ProcessRequest>(DocumentCaptureSessionCommand.AddProcessRequest);

            machine.Configure(CaptureSessionState.Created)
                .Permit(DocumentCaptureSessionCommand.AddProcessRequest, CaptureSessionState.InProgress)
                .OnExit(onExit);

            machine.Configure(CaptureSessionState.InProgress)
                .OnEntryFrom(processRequestTrigger, onAddProcessRequest)
                .PermitReentryIf(DocumentCaptureSessionCommand.AddProcessRequest, canAddRequest)
                .PermitIf(DocumentCaptureSessionCommand.Finish, CaptureSessionState.Finished, canFinish)
                .OnExit(onExit);

            machine.OnUnhandledTrigger(unhandledTriggerAction);
        }

        public Page[] Pages => pages.ToArray();


        public override ProcessRequest[] ProcessRequests
        {
            get { return Pages.SelectMany(p => p.ProcessRequests).ToArray(); }
        }

        public override ProcessResult[] ProcessResults
        {
            get { return Pages.SelectMany(p => p.ProcessResults).ToArray(); }
        }

        public override CaptureSessionResult Result
        {
            get
            {
                if (canFinish())
                    return pages.All(p => p.Result == PageResult.Passed) ? CaptureSessionResult.Passed : CaptureSessionResult.Failed;
                return CaptureSessionResult.Undetermined;
            }
        }

        public void AddProcessRequest(ProcessRequest request)
        {
            Console.WriteLine($"Process Request for : {request.Data} - Added to CaptureSession: {Id}");
            machine.Fire(processRequestTrigger, request);
            Console.WriteLine("--------ProcessRequest finished");
            if (canFinish())
                Finish();
        }

        private bool canAddRequest()
        {
            return !canFinish();
        }

        private bool canFinish()
        {
            var lastPage = pages.OrderBy(p => p.StepNumber).LastOrDefault(p => p.State == PageState.Finished);

            if (pages.Count == pageLimit && pages.All(p => p.State == PageState.Finished) || lastPage != null && lastPage.Result == PageResult.Failed)
                return true;
            return false;
        }

        private void Finish()
        {
            machine.Fire(DocumentCaptureSessionCommand.Finish);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("*** THANK YOU ***");
            Console.WriteLine($"Your CaptureSession {Result}");
            Console.WriteLine("");
            Console.WriteLine("CaptureSession Data:");
            foreach (var page in pages)
            {
                Console.WriteLine($"-> Page Id {page.Id}");
                Console.WriteLine($"-> Page Result {page.Result}");
                Console.WriteLine($"-> Page State {page.State}");
                Console.WriteLine($"-> Page has {page.ProcessResults.Length} process result(s)");
                foreach (var processResult in page.ProcessResults)
                {
                    var processRequest = page.ProcessRequests.First(r => r.Id == processResult.RequestId);
                    Console.WriteLine($"->  -> '{processResult.ProcessedData}' was the result of '{processRequest.Data}' ProcessRequest");
                }
                Console.WriteLine("------------------------------------");
            }
        }

        private void onAddProcessRequest(ProcessRequest request)
        {
            var stepNumber = pages.Max(x => (int?) x.StepNumber) ?? 0;
            var page = pages.OrderBy(p => p.StepNumber).LastOrDefault(p => p.State == PageState.InProgress);
            if (page == null)
                pages.Add(Page.Create(request, stepNumber + 1));
            else
                page.AddProcessRequest(request);
        }


        private void onExit()
        {
            //Console.WriteLine("onExit <-");
        }

        private void unhandledTriggerAction(CaptureSessionState _state, DocumentCaptureSessionCommand command)
        {
            throw new Exception($"The CaptureSession is {_state}. It's not allowed to perform {command}");
        }
    }
}