using System;

namespace ConsoleApp.Model
{
    internal class ProcessRequest
    {
        public static ProcessRequest Create(string data)
        {
            return new ProcessRequest(Guid.NewGuid(), data);
        }

        private ProcessRequest(Guid id, string data)
        {
            Id = id;
            Data = data;
        }

        public Guid Id { get; }
        public string Data { get; }
    }
}