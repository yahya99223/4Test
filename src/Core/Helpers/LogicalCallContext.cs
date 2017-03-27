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
                //Console.WriteLine("Plain Task {0} - Added", task.Id);

                task.ContinueWith(doneTask =>
                {
                    lock (locker)
                    {
                        tasks.Remove(doneTask);
                        //Console.WriteLine("Plain Task {0} - Removed", doneTask.Id);
                        if (tasks.Any())
                        {
                            //Console.WriteLine("Scope - Disposing");
                            Scope.Dispose();
                            //Console.WriteLine("Scope - Disposed");
                        }
                    }
                });
            }
        }

        public Task AddActionAsTask(Action action)
        {
            var t = new Task(action);
            lock (locker)
            {
                tasks.Add(t);
                //Console.WriteLine("Action Task {0} - Added", t.Id);
            }
            t.ContinueWith(doneTask =>
            {
                lock (locker)
                {
                    tasks.Remove(doneTask);
                    //Console.WriteLine("Action Task {0} - Removed", doneTask.Id);
                    if (!tasks.Any())
                    {
                        //Console.WriteLine("Scope - Disposing");
                        Scope.Dispose();
                        //Console.WriteLine("Scope - Disposed");
                    }
                }
            });

            //Console.WriteLine("Action Task {0} - Starting", t.Id);
            t.Start();
            //Console.WriteLine("Action Task {0} - Started", t.Id);

            return t;
        }
    }
}