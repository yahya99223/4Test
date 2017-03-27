using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Helpers;
using Core.Services;
using IoC;

namespace ConsoleApp
{
    class Program
    {
        private static IServiceResolver serviceResolver;

        static void Main(string[] args)
        {
            var executeFolder = AppDomain.CurrentDomain.BaseDirectory;
            serviceResolver = ServiceResolverFactory.GetServiceResolver(executeFolder);

            listenToRequests();

            showStatistics();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press Enter to exit ..!");

            Console.ReadLine();
        }

        private static void listenToRequests()
        {
            var actions = new List<string>
            {
                "sync", "async"
            };
            string requestType;
            do
            {
                requestType = Console.ReadLine()?.ToLowerInvariant();
                if (actions.Contains(requestType))
                {
                    StaticInfo.BeginWebRequests += 1;

                    var requestId = Guid.NewGuid();
                    CallContext.LogicalSetData("CallContextId", requestId);

                    var scope = serviceResolver.StartScope();
                    var type = requestType;
                    Task.Factory.StartNew(() =>
                        {
                            doTheWork(type);
                        }
                        , CancellationToken.None
                        , TaskCreationOptions.None
                        , TaskScheduler.Current
                    ).ContinueWith(oldTask =>
                    {
                        scope.Dispose();
                    });

                    StaticInfo.EndWebRequests += 1;
                }

                if (requestType == "print")
                    showStatistics();
            } while (requestType != "exit");
        }

        private static void doTheWork(string type)
        {
            switch (type)
            {
                case "sync":
                    doSyncWork();
                    break;

                case "async":
                    Task.Factory.StartNew(async () =>
                    {
                        await doAsyncWork();
                    });
                    break;
            }
        }

        private static void doSyncWork()
        {
            var userService = serviceResolver.Resolve<IUserService>();
            var user = Core.Model.User.Create("Sameer");
            userService.Add(user);
        }

        private static async Task doAsyncWork()
        {
            var userService = serviceResolver.Resolve<IUserService>();
            var user = Core.Model.User.Create("ASameer");
            await userService.AddAsync(user);
        }

        private static void showStatistics()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("===============================================================");

            Console.WriteLine(string.Format("Begin Requests :{0}", StaticInfo.BeginWebRequests));
            Console.WriteLine(string.Format("End Requests :{0}", StaticInfo.EndWebRequests));

            Console.WriteLine("-----");

            Console.WriteLine(string.Format("Started UnitOfWorks :{0}", StaticInfo.StartedUnitOfWorks));
            Console.WriteLine(string.Format("Commited UnitOfWorks :{0}", StaticInfo.CommitedUnitOfWorks));
            Console.WriteLine(string.Format("Disposed UnitOfWorks :{0}", StaticInfo.DisposedUnitOfWorks));

            Console.WriteLine("-----");
            Console.WriteLine(string.Format("Users :{0}", StaticInfo.Users));

            Console.WriteLine("-----");
            Console.WriteLine(string.Format("Exception :{0}", StaticInfo.Exception));

            Console.WriteLine("===============================================================");
        }
    }
}
