using System;
using System.Collections.Generic;

namespace Core.DomainModel
{
    public class JourneyEntryDefinition
    {
        private int order;
        private bool isOptional;
        private EntryStepDefinition[] stepDefinitions;

        public JourneyEntryDefinition(Guid id, int order, EntryType entryType, bool isOptional, EntryStepDefinition[] stepDefinitions)
        {
            Id = id;
            EntryType = entryType;
            this.order = order;
            this.isOptional = isOptional;
            this.stepDefinitions = stepDefinitions;
        }

        public Guid Id { get; private set; }
        public EntryType EntryType { get; private set; }

        public int Order
        {
            get { return order; }
            set
            {
                //ToDo Add DomainEvent
                order = value;
            }
        }

        public bool IsOptional
        {
            get { return isOptional; }
            set
            {
                //ToDo Add DomainEvent
                isOptional = value;
            }
        }

        public EntryStepDefinition[] StepDefinitions
        {
            get { return stepDefinitions; }
            set
            {
                //ToDo Add DomainEvent
                stepDefinitions = value;
            }
        }

        public Dictionary<string, string> AvailableEntrySteps()
        {
            switch (EntryType)
            {
                case EntryType.FacePhoto:
                case EntryType.Fingerprint:
                case EntryType.GpsCoordinates:
                case EntryType.LivenessDetection:
                case EntryType.VideoChat:
                    return getAvailableStepsForOneStepTypes();
                case EntryType.IdentityCard:
                case EntryType.Hologram:
                    return getAvailableStepsForTwoStepsTypes();
                case EntryType.Passport:
                case EntryType.PaperDocument:
                    return getAvailableStepsForMultiStepsTypes();
            }
            return new Dictionary<string, string>();
        }

        private Dictionary<string, string> getAvailableStepsForOneStepTypes()
        {
            return new Dictionary<string, string>
            {
                {string.Format("{0}_{1}", Order, EntryType), EntryType.ToString()},
            };
        }

        private Dictionary<string, string> getAvailableStepsForTwoStepsTypes()
        {
            return new Dictionary<string, string>
            {
                {string.Format("{0}_{1}_FrontSide", Order, EntryType), string.Format(@"{0} FrontSide", EntryType)},
                {string.Format("{0}_{1}_BackSide", Order, EntryType), string.Format(@"{0} BackSide", EntryType)},
            };
        }

        private Dictionary<string, string> getAvailableStepsForMultiStepsTypes()
        {
            var maxSteps = 7;
            var result = new Dictionary<string, string>();
            for (int i = 1; i <= maxSteps; i++)
            {
                result.Add(string.Format("{0}_{1}_Page_{2}", Order, EntryType, i), string.Format(@"{0} Page({1})", EntryType, i));
            }
            return result;
        }
    }
}