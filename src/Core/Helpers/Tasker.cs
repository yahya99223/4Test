using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class Tasker
    {
        private static readonly IList<LogicalCallContext> callContexts = new List<LogicalCallContext>();
        private static readonly object locker = new object();

        public static void RegisterScope(Guid requestKey, IDisposable scope, Task task = null)
        {
            lock (locker)
            {
                var context = new LogicalCallContext(requestKey, scope);
                if (task != null)
                {
                    context.AddTask(task);

                    if (task.Status == TaskStatus.Created)
                        task.Start();
                }
                callContexts.Add(context);
            }
        }

        public static async Task Run( Guid requestKey, Action action)
        {
            LogicalCallContext context;
            lock (locker)
            {
                context = callContexts.FirstOrDefault(c => c.RequestId == requestKey);
            }
            if (context != null)
                await context.AddActionAsTask(action);
            else
                throw new Exception("Out of context");
        }
    }
}
