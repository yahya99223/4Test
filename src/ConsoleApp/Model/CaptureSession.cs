using System;

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
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Capture Session state is {value}");
                Console.WriteLine();
                state = value;
            }
        }

        public abstract ProcessRequest[] ProcessRequests { get; }
        public abstract ProcessResult[] ProcessResults { get; }
        public abstract CaptureSessionResult Result { get; }
    }
}