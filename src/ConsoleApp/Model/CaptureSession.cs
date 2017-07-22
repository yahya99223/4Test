using System;
using Stateless;

namespace ConsoleApp.Model
{
    internal abstract class CaptureSession
    {
        private CaptureSessionState state;

        protected CaptureSession(Guid id, CaptureSessionState state)
        {
            Id = id;
            this.state = state;
        }

        public Guid Id { get; }

        public CaptureSessionState State
        {
            get => state;
            protected set
            {
                Console.WriteLine($"Capture Session state will change from {state} into {value}");
                state = value;
            }
        }

        public abstract ProcessRequest[] ProcessRequests { get; }
        public abstract ProcessResult[] ProcessResults { get; }
    }
}