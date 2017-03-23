using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class LogicalCallContext
    {
        private IList<Task> tasks;
        private readonly object locker = new object();


        public LogicalCallContext(Guid requestId, IDisposable scope)
        {
            RequestId = requestId;
            Scope = scope;
            this.tasks = new List<Task>();
        }

        public Guid RequestId { get; private set; }
        public IDisposable Scope { get; private set; }

        public Task[] Tasks
        {
            get { return tasks.ToArray(); }
        }

        public void AddTask(Task task)
        {
            lock (locker)
            {
                tasks.Add(task);
            }
        }

        public Task AddActionAsTask(Action action)
        {
            var t = new Task(action);
            lock (locker)
            {
                tasks.Add(t);
            }
            t.ContinueWith(doneTask =>
            {
                lock (locker)
                {
                    tasks.Remove(doneTask);
                    if (!tasks.Any())
                        Scope.Dispose();
                }
            });
            t.Start();
            return t;
        }
    }
}