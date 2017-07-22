using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace ConsoleApp.Model
{
    internal class DocumentCaptureSession : CaptureSession
    {
        private readonly StateMachine<CaptureSessionState, DocumentCaptureSessionCommand> machine;

        public DocumentCaptureSession(Guid id, CaptureSessionState state, IList<Page> pages) : base(id, state)
        {
            Pages = pages ?? new List<Page>();
            machine = new StateMachine<CaptureSessionState, DocumentCaptureSessionCommand>(() => State, s => State = s);
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

        public void AddProcessRequest(ProcessRequest request)
        {

        }

        public void AddProcessResult(PageProcessResult processResult)
        {
        }

        public static DocumentCaptureSession Create()
        {
            return new DocumentCaptureSession(Guid.NewGuid(), CaptureSessionState.Created, null);
        }
    }
}