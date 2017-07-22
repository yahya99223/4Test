using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Model
{
    internal class DocumentCaptureSession : CaptureSession
    {
        public DocumentCaptureSession(Guid id, IList<Page> pages) : base(id)
        {
            Pages = pages ?? new List<Page>();
        }

        public IList<Page> Pages { get; set; }

        public override ProcessRequest[] ProcessRequests
        {
            get { return Pages.SelectMany(p => p.ProcessRequests).ToArray(); }
        }

        public override ProcessResult[] ProcessResults
        {
            get { return Pages.SelectMany(p => p.ProcessResults).ToArray(); }
        }
    }
}