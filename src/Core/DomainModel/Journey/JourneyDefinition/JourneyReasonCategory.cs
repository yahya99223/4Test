using System;
using System.Collections.Generic;

namespace Core.DomainModel
{
    public class JourneyReasonCategory 
    {
        private string name;
        private IList<JourneyReason> journeyReasons;

        public JourneyReasonCategory(Guid id, string name, IList<JourneyReason> journeyReasons = null)
        {
            Id = id;
            this.name = name;
            this.journeyReasons = journeyReasons ?? new List<JourneyReason>();
        }


        public Guid Id { get; private set; }

        public string Name
        {
            get { return name; }
            set
            {
                //ToDo Add DomainEvent
                name = value;
            }
        }

        public IList<JourneyReason> JourneyReasons
        {
            get { return journeyReasons; }
            set
            {
                //ToDo Add DomainEvent
                journeyReasons = value;
            }
        }
    }
}