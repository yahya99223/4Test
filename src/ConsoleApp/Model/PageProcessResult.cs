using System;

namespace ConsoleApp.Model
{
    internal class PageProcessResult : ProcessResult
    {
        public PageProcessResult(Guid id, Guid requestId, int stepNumber, string processedData) : base(id, requestId, stepNumber)
        {
            ProcessedData = processedData;
        }

        public string ProcessedData { get; }

    }
}