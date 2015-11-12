using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public class JourneyStarted : IDomainEvent
    {
        public Journey Journey { get; private set; }

        public JourneyStarted(Journey journey)
        {
            Journey = journey;
        }
    }



    public class JourneyIsProceeding : IDomainEvent
    {
        public Journey Journey { get; private set; }

        public JourneyIsProceeding(Journey journey)
        {
            Journey = journey;
        }
    }



    public class JourneyIsCanceled : IDomainEvent
    {
        public Journey Journey { get; private set; }

        public JourneyIsCanceled(Journey journey)
        {
            Journey = journey;
        }
    }



    public class JourneyIsFinished : IDomainEvent
    {
        public Journey Journey { get; private set; }

        public JourneyIsFinished(Journey journey)
        {
            Journey = journey;
        }
    }
}
