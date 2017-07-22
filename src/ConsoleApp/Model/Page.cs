using System;
using System.Collections.Generic;

namespace ConsoleApp.Model
{
    internal class Page
    {
        public Page(Guid id, IList<ProcessRequest> processRequests, IList<PageProcessResult> processResults)
        {
            Id = id;
            ProcessRequests = processRequests ?? new List<ProcessRequest>();
            ProcessResults = processResults ?? new List<PageProcessResult>();
        }

        public Guid Id { get; }
        public IList<ProcessRequest> ProcessRequests { get; }
        public IList<PageProcessResult> ProcessResults { get; }
    }
}