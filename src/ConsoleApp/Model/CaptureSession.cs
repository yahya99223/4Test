using System;
using Stateless;

namespace ConsoleApp.Model
{
    internal abstract class CaptureSession
    {
        protected CaptureSession(Guid id, CaptureSessionState state)
        {
            Id = id;
            State = state;
        }

        public Guid Id { get; }
        public CaptureSessionState State { get; protected set; }
        public abstract ProcessRequest[] ProcessRequests { get; }
        public abstract ProcessResult[] ProcessResults { get; }
    }

}