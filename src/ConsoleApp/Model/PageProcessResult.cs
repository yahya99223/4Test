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

        public static PageProcessResult Create(ProcessRequest request)
        {
            string processedData = Reverse(request.Data);
            return new PageProcessResult(Guid.NewGuid(), request.Id, request.StepNumber, processedData);
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}