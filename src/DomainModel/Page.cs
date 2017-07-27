using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    public class Page
    {
        internal static Page Create(ProcessRequest request, int stepNumber)
        {
            var page = new Page(Guid.NewGuid(), stepNumber, PageState.Created, PageResult.Undetermined, null, null);
            page.AddProcessRequest(request);
            return page;
        }

        private readonly IList<ProcessRequest> processRequests;
        private readonly IList<PageProcessResult> processResults;

        private PageStateAbstraction machine;

        private Page(Guid id, int stepNumber, PageState state, PageResult result, IList<ProcessRequest> processRequests, IList<PageProcessResult> processResults)
        {
            Id = id;
            Result = result;
            StepNumber = stepNumber;
            this.processRequests = processRequests ?? new List<ProcessRequest>();
            this.processResults = processResults ?? new List<PageProcessResult>();

            machine = PageStateAbstraction.GetState(state, this);
        }

        public Guid Id { get; }
        public int StepNumber { get; }
        public PageResult Result { get; private set; }
        public ProcessRequest[] ProcessRequests => processRequests.ToArray();
        public PageProcessResult[] ProcessResults => processResults.ToArray();
        public PageState State => machine.State;

        public void AddProcessRequest(ProcessRequest request)
        {
            Console.WriteLine($"Process Request for : {request.Data} - Added to page number: {StepNumber}");
            machine.Proceed();
            onAddProcessRequest(request);
        }

        public void AddProcessResult(PageProcessResult processResult)
        {
            processResults.Add(processResult);
            if (processResult.ProcessedData.Length > 3)
                Result = PageResult.Passed;
            if (processResults.Count >= 3 && Result != PageResult.Passed)
                Result = PageResult.Failed;

            if (canFinish())
                Finish();
        }

        internal bool canFinish()
        {
            return processResults.Count >= 3 || Result == PageResult.Passed;
        }

        internal bool canRetry()
        {
            return processResults.Count < 3;
        }

        internal void setState(PageStateAbstraction newState)
        {
            Console.WriteLine($"Page state will change from {State} into {newState.State}");
            machine = newState;
        }

        private void Finish()
        {
            machine.Finish();
        }

        private void onAddProcessRequest(ProcessRequest request)
        {
            Console.WriteLine($"Process Request for : {request.Data} - Received to page number: {StepNumber}");

            processRequests.Add(request);

            var result = PageProcessResult.Create(request, StepNumber);
            AddProcessResult(result);
        }
    }
}