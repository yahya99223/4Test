using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class Tasker
    {
        private static readonly IList<LogicalCallContext> callContexts = new List<LogicalCallContext>();
        private static readonly object locker = new object();

        public static void RegisterScope(Guid requestKey, IDisposable scope)
        {
            lock (locker)
            {
                var context = new LogicalCallContext(requestKey, scope);                
                callContexts.Add(context);
            }
        }

        public static void Run(Task task)
        {
            lock (locker)
            {
                var callContextId = (Guid) CallContext.LogicalGetData("CallContextId");
                var context = callContexts.FirstOrDefault(c => c.RequestId == callContextId);
                if (context != null && task != null)
                {
                    context.AddTask(task);

                    if (task.Status == TaskStatus.Created)
                    {
                        Console.WriteLine("Plain Task {0} - Starting", task.Id);
                        task.Start();
                        Console.WriteLine("Plain Task {0} - Started", task.Id);
                    }
                }
                else
                    throw new Exception("Out of context");
            }
        }

        public static Task Run(Action action)
        {
            LogicalCallContext context;
            lock (locker)
            {
                var callContextId = (Guid) CallContext.LogicalGetData("CallContextId");
                context = callContexts.FirstOrDefault(c => c.RequestId == callContextId);
            
            if (context != null)
                return context.AddActionAsTask(action);
            else
                throw new Exception("Out of context");
            }
        }
    }
}