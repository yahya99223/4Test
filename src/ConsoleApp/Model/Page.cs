using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace ConsoleApp.Model
{
    internal class Page
    {
        internal static Page Create(ProcessRequest request, int stepNumber)
        {
            var page = new Page(Guid.NewGuid(), stepNumber, PageState.Created, PageResult.Undetermined, null, null);
            page.AddProcessRequest(request);
            return page;
        }

        private readonly StateMachine<PageState, PageCommand> machine;
        private readonly IList<ProcessRequest> processRequests;
        private readonly StateMachine<PageState, PageCommand>.TriggerWithParameters<ProcessRequest> processRequestTrigger;
        private readonly IList<PageProcessResult> processResults;
        private PageState state;

        private Page(Guid id, int stepNumber, PageState state, PageResult result, IList<ProcessRequest> processRequests, IList<PageProcessResult> processResults)
        {
            Id = id;
            this.state = state;
            Result = result;
            StepNumber = stepNumber;
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

            machine.OnUnhandledTrigger(unhandledTriggerAction);
        }

        public Guid Id { get; }
        public int StepNumber { get; }
        public PageResult Result { get; private set; }
        public ProcessRequest[] ProcessRequests => processRequests.ToArray();
        public PageProcessResult[] ProcessResults => processResults.ToArray();

        public PageState State
        {
            get => state;
            internal set
            {
                Console.WriteLine($"Page state will change from {state} into {value}");
                state = value;
            }
        }

        public void AddProcessRequest(ProcessRequest request)
        {
            Console.WriteLine($"Process Request for : {request.Data} - Added to page number: {StepNumber}");
            machine.Fire(processRequestTrigger, request);

            if (canFinish())
                Finish();
        }

        public void AddProcessResult(PageProcessResult processResult)
        {
            processResults.Add(processResult);
            if (processResult.ProcessedData.Length > 3)
                Result = PageResult.Passed;
            if (processResults.Count >= 3 && Result != PageResult.Passed)
                Result = PageResult.Failed;
        }

        private bool canFinish()
        {
            return processResults.Count >= 3 || Result == PageResult.Passed;
        }

        private bool canRetry()
        {
            return processResults.Count < 3;
        }

        private void Finish()
        {
            machine.Fire(PageCommand.Finish);
        }

        private void onAddProcessRequest(ProcessRequest request)
        {
            Console.WriteLine($"Process Request for : {request.Data} - Received to page number: {StepNumber}");

            processRequests.Add(request);

            var result = PageProcessResult.Create(request, StepNumber);
            AddProcessResult(result);
        }

        private void onExit()
        {
            //Console.WriteLine("onExit <-");
        }

        private void unhandledTriggerAction(PageState _state, PageCommand command)
        {
            throw new Exception($"The page is {_state}. It's not allowed to perform {command}");
        }
    }
}