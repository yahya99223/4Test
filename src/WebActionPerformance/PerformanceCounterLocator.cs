using System.Diagnostics;

namespace WebActionPerformance
{
    public class PerformanceCounterLocator
    {
        private static object m_SyncRoot = new object();

        private const string CategoryName = "Web Action Performance";

        private PerformanceCounterLocator()
        {
            WebActionOperation = new PerformanceMonitorOperation(CategoryName, "Web Action");
            WebActionOperationError = new PerformanceMonitorOperation(CategoryName, "Web Action Error");
        }
        
        public PerformanceMonitorOperation WebActionOperation { get; private set; }
        public PerformanceMonitorOperation WebActionOperationError { get; private set; }

        private static PerformanceCounterLocator _mInstance;
        public static PerformanceCounterLocator Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    lock (m_SyncRoot)
                    {
                        if (_mInstance == null)
                        {
                            _mInstance = new PerformanceCounterLocator();
                        }
                    }
                }

                return _mInstance;
            }
        }

        public void CreateCounters()
        {
            var countersToCreate = new CounterCreationDataCollection();            
            WebActionOperation.RegisterCountersForCreation(countersToCreate);
            WebActionOperationError.RegisterCountersForCreation(countersToCreate);

            PerformanceCounterCategory.Create(CategoryName, CategoryName, PerformanceCounterCategoryType.MultiInstance, countersToCreate);
        }

        public void DeleteCounters()
        {
            PerformanceCounterCategory.Delete(CategoryName);
        }
    }
}
