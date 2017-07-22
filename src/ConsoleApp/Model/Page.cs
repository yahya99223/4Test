using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace ConsoleApp.Model
{
    internal class Page
    {
        private readonly StateMachine<PageState, PageCommand> machine;
        private readonly IList<ProcessRequest> processRequests;
        private readonly StateMachine<PageState, PageCommand>.TriggerWithParameters<ProcessRequest> processRequestTrigger;
        private readonly IList<PageProcessResult> processResults;
        private PageState state;

        private Page(Guid id, IList<ProcessRequest> processRequests, IList<PageProcessResult> processResults, PageState state, PageResult result)
        {
            Id = id;
            this.state = state;
            Result = result;
            this.processRequests = processRequests ?? new List<ProcessRequest>();
            this.processResults = processResults ?? new List<PageProcessResult>();

            machine = new StateMachine<PageState, PageCommand>(() => State, s => State = s);

            processRequestTrigger = machine.SetTriggerParameters<ProcessRequest>(PageCommand.TryProcess);

            machine.Configure(PageState.Created)
                .Permit(PageCommand.TryProcess, PageState.InProgress)
                .OnExit(onExit);


            machine.Configure(PageState.InProgress)
                .OnEntryFrom(processRequestTrigger, onAddProcessRequest)
                .PermitReentryIf(PageCommand.TryProcess, canRetry)
                .PermitIf(PageCommand.Finish, PageState.Finished, canFinish)
                .OnExit(onExit);
        }

        public Guid Id { get; }

        public PageState State
        {
            get { return state; }
            private set
            {
                Console.WriteLine($"Page state will change from {state} into {value}");
                state = value;
            }
        }

        public PageResult Result { get; private set; }
        public ProcessRequest[] ProcessRequests => processRequests.ToArray();
        public PageProcessResult[] ProcessResults => processResults.ToArray();

        private bool canRetry()
        {
            return processResults.Count < 3;
        }

        private void onExit()
        {
            Console.WriteLine("onExit <-");
        }

        private bool canFinish()
        {
            return processResults.Count >= 3 || Result == PageResult.Passed;
        }

        public void AddProcessRequest(ProcessRequest request)
        {
            Console.WriteLine($"Process Request for : {request.Data} - Added");
            machine.Fire(processRequestTrigger, request);
        }

        private void onAddProcessRequest(ProcessRequest request)
        {
            Console.WriteLine($"Process Request for : {request.Data} - Received");

            processRequests.Add(request);

            var result = PageProcessResult.Create(request);
            AddProcessResult(result);
        }

        public void AddProcessResult(PageProcessResult processResult)
        {
            processResults.Add(processResult);
            if (processResult.ProcessedData.Length > 3)
                Result = PageResult.Passed;
            if (processResults.Count >= 3 && Result != PageResult.Passed)
                Result = PageResult.Failed;

            if (processResults.Count >= 3 || Result == PageResult.Passed)
                Finish();
        }

        private void Finish()
        {
            machine.Fire(PageCommand.Finish);
        }


        public static Page Create(ProcessRequest request)
        {
            var page = new Page(Guid.NewGuid(), null, null, PageState.Created, PageResult.Undetermined);
            page.AddProcessRequest(request);
            return page;
        }
    }
}