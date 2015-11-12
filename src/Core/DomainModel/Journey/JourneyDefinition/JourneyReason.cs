using System;

namespace Core.DomainModel
{
    public class JourneyReason 
    {
        private string name;
        private Guid[] journeyDefinitionIds;
        private Guid? journeyReasonCategoryId;

        public JourneyReason(Guid id, Guid[] journeyDefinitionIds, string name, Guid? journeyReasonCategoryId = null)
        {
            Id = id;
            this.name = name;
            this.journeyReasonCategoryId = journeyReasonCategoryId;
            this.journeyDefinitionIds = journeyDefinitionIds;
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

        public Guid[] JourneyDefinitionIds
        {
            get { return journeyDefinitionIds; }
            set
            {
                //ToDo Add DomainEvent
                journeyDefinitionIds = value;
            }
        }

        public Guid? JourneyReasonCategoryId
        {
            get { return journeyReasonCategoryId; }
            set
            {
                //ToDo Add DomainEvent
                journeyReasonCategoryId = value;
            }
        }
    }
}