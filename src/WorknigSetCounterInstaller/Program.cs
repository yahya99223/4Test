using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorknigSetCounterInstaller
{
    class Program
    {
        private const string CategoryName = "IDS Memory Usage";
        protected const string KbCounterName_MemoryUsage = "KB Memory Usage";
        protected const string MbCounterName_MemoryUsage = "MB Memory Usage";

        static void Main(string[] args)
        {
            /*var countersToCreate = new CounterCreationDataCollection();
            countersToCreate.Add(CreateCounter(KbCounterName_MemoryUsage, PerformanceCounterType.NumberOfItems64));
            countersToCreate.Add(CreateCounter(MbCounterName_MemoryUsage, PerformanceCounterType.NumberOfItems64));

            PerformanceCounterCategory.Create(CategoryName, CategoryName, PerformanceCounterCategoryType.MultiInstance, countersToCreate);*/
            

            while (true)
            {
                Thread.Sleep(300);
                foreach (var process in Process.GetProcesses())
                {
                    var kbMmemoryUsage = new PerformanceCounter(CategoryName, KbCounterName_MemoryUsage, process.ProcessName, false);
                    kbMmemoryUsage.RawValue = process.WorkingSet64 / 1024;

                    var mbMemoryUsage = new PerformanceCounter(CategoryName, MbCounterName_MemoryUsage, process.ProcessName, false);
                    mbMemoryUsage.RawValue = (process.WorkingSet64 / 1024)/1024;
                }
            }
        }

        private static CounterCreationData CreateCounter(string counterName, PerformanceCounterType counterType)
        {
            return new CounterCreationData(counterName, counterName, counterType);
        }
    }
}
