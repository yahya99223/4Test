using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WebActionPerformance
{
    public class PerformanceMonitorOperation
    {
        private readonly object mSyncRoot = new object();
        private bool mAreCountersInitializedForRuntime;

        protected const string CounterName_AverageOperationTime = "{0}: Average Operation Time";
        protected const string CounterName_AverageOperationTimeBase = "{0}: Average Operation Time Base";
        protected const string CounterName_OperationCount = "{0}: Operation Count";
        protected const string CounterName_OperationsPerSecond = "{0}: Operations Per Second";
        protected const string CounterName_MemoryUsage = "{0}: Memory Usage";
        

        public PerformanceMonitorOperation(string categoryName, string operationName)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentException("categoryName is null or empty.", "categoryName");
            if (string.IsNullOrEmpty(operationName))
                throw new ArgumentException("operationName is null or empty.", "operationName");

            CategoryName = categoryName;
            OperationName = operationName;
        }

        public string CategoryName { get; }
        public string OperationName { get; }
        protected PerformanceCounter MemoryUsage { get; private set; }
        protected PerformanceCounter OperationCount { get; private set; }
        protected PerformanceCounter OperationsPerSecond { get; private set; }
        protected PerformanceCounter AverageOperationTime { get; private set; }
        protected PerformanceCounter AverageOperationTimeBase { get; private set; }

        /// <summary>
        ///     Use this method to record the duration for an operation.
        /// </summary>
        /// <param name="duration">Number of ticks for the operation.</param>
        public void RecordOperation(long duration)
        {
            CheckIsInitializedForRuntime();
            OperationCount.Increment();
            OperationsPerSecond.Increment();
            AverageOperationTime.IncrementBy(duration);
            AverageOperationTimeBase.Increment();
            MemoryUsage.RawValue = Process.GetCurrentProcess().WorkingSet64/1024;

            /*
            foreach (var process in Process.GetProcesses())
            {
                MemoryUsage.InstanceName = process.ProcessName;
                MemoryUsage.RawValue = process.WorkingSet64/1024;
            }
            */
        }

        protected void InitializeCountersForRuntime()
        {
            AverageOperationTime = GetPerformanceCounterInstance(CounterName_AverageOperationTime);
            AverageOperationTimeBase = GetPerformanceCounterInstance(CounterName_AverageOperationTimeBase);
            OperationCount = GetPerformanceCounterInstance(CounterName_OperationCount);
            OperationsPerSecond = GetPerformanceCounterInstance(CounterName_OperationsPerSecond);
            MemoryUsage = GetPerformanceCounterInstance(CounterName_MemoryUsage);

            OnInitializeCountersForRuntime();

            mAreCountersInitializedForRuntime = true;
        }

        public void RegisterCountersForCreation(CounterCreationDataCollection countersToCreate)
        {
            countersToCreate.Add(CreateCounter(string.Format(CounterName_OperationCount, OperationName), PerformanceCounterType.NumberOfItems64));
            countersToCreate.Add(CreateCounter(string.Format(CounterName_OperationsPerSecond, OperationName), PerformanceCounterType.RateOfCountsPerSecond64));
            countersToCreate.Add(CreateCounter(string.Format(CounterName_AverageOperationTime, OperationName), PerformanceCounterType.AverageTimer32));
            countersToCreate.Add(CreateCounter(string.Format(CounterName_AverageOperationTimeBase, OperationName), PerformanceCounterType.AverageBase));
            countersToCreate.Add(CreateCounter(string.Format(CounterName_MemoryUsage, OperationName), PerformanceCounterType.NumberOfItems64));

            BeforeCreateCounters(countersToCreate);
        }

        protected virtual void BeforeCreateCounters(CounterCreationDataCollection countersToCreate)
        {
            // override this method if you want to create other counters for this category
        }

        private CounterCreationData CreateCounter(string counterName, PerformanceCounterType counterType)
        {
            return new CounterCreationData(counterName, counterName, counterType);
        }

        public void Delete()
        {
            PerformanceCounterCategory.Delete(CategoryName);
        }

        protected virtual void OnInitializeCountersForRuntime()
        {
            // override this method if have created other performance counters that need to 
            // be initialized for runtime 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="counterNameTemplate"></param>
        /// <returns>the result of work</returns>
        private PerformanceCounter GetPerformanceCounterInstance(string counterNameTemplate)
        {
            var counterName = string.Format(counterNameTemplate, OperationName);

            Console.WriteLine("Created counter instance for '{0}'.", counterName);

            return new PerformanceCounter(CategoryName, counterName, false);
        }

        private void CheckIsInitializedForRuntime()
        {
            if (mAreCountersInitializedForRuntime == false)
            {
                lock (mSyncRoot)
                {
                    if (mAreCountersInitializedForRuntime == false)
                    {
                        Console.WriteLine("Initializing counter instances for operation '{0}'.", OperationName);
                        InitializeCountersForRuntime();
                    }
                }
            }
        }

        /// <summary>
        ///     Record an operation without a duration.  Use this method to
        ///     record an exception.
        /// </summary>
        public void RecordOperation()
        {
            RecordOperation(0);
        }
    }
}