using System;

namespace DomainModel
{
    public class PageProcessResult : ProcessResult
    {
        internal static PageProcessResult Create(ProcessRequest request, int stepNumber)
        {
            var processedData = Reverse(request.Data);
            return new PageProcessResult(Guid.NewGuid(), request.Id, stepNumber, processedData);
        }

        private PageProcessResult(Guid id, Guid requestId, int stepNumber, string processedData) : base(id, requestId, stepNumber)
        {
            ProcessedData = processedData;
        }

        public string ProcessedData { get; }

        private static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}