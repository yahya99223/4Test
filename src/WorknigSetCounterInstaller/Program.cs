using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorknigSetCounterInstaller
{
    class Program
    {
        private const string CategoryName = "KB Memory Usage";
        protected const string CounterName_MemoryUsage = "{0}: Memory Usage";

        static void Main(string[] args)
        {
            var countersToCreate = new CounterCreationDataCollection();

            countersToCreate.Add(CreateCounter(string.Format(CounterName_MemoryUsage, "WebActionOperation"), PerformanceCounterType.NumberOfItems64));

            PerformanceCounterCategory.Create(CategoryName, CategoryName, PerformanceCounterCategoryType.MultiInstance, countersToCreate);
        }

        private static CounterCreationData CreateCounter(string counterName, PerformanceCounterType counterType)
        {
            return new CounterCreationData(counterName, counterName, counterType);
        }
    }
}
