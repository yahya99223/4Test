using System;

namespace ConsoleApp.Model
{
    internal abstract class CaptureSession
    {
        protected CaptureSession(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public abstract ProcessRequest[] ProcessRequests { get; }
        public abstract ProcessResult[] ProcessResults { get; }
    }
}