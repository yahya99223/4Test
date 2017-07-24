using System;

namespace DomainModel
{
    public class ProcessRequest
    {
        internal static ProcessRequest Create(string data)
        {
            return new ProcessRequest(Guid.NewGuid(), data);
        }

        protected ProcessRequest(Guid id, string data)
        {
            Id = id;
            Data = data;
        }

        public Guid Id { get; }
        public string Data { get; }
    }
}