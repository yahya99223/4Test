using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DomainModel
{
    public class JourneyDefinition
    {
        private bool isOrderOptional;
        private JourneyEntryDefinition[] journeyEntryDefinitions;
        private string name;

        public JourneyDefinition(Guid id, string name, bool isOrderOptional, JourneyEntryDefinition[] journeyEntryDefinitions)
        {
            Id = id;
            Name = name;
            this.isOrderOptional = isOrderOptional;
            this.journeyEntryDefinitions = journeyEntryDefinitions;
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

        public bool IsOrderOptional
        {
            get { return isOrderOptional; }
            set
            {
                //ToDo Add DomainEvent
                isOrderOptional = value;
            }
        }

        public JourneyEntryDefinition[] JourneyEntryDefinitions
        {
            get { return journeyEntryDefinitions; }
            set
            {
                //ToDo Add DomainEvent
                journeyEntryDefinitions = value;
            }
        }

        public bool IsItFinalStep(string stepKey)
        {
            var allowedSteps = GetAllowedSteps();
            var nextIndex = allowedSteps.FindIndex(x => string.Equals(stepKey, x, StringComparison.InvariantCultureIgnoreCase));
            return nextIndex == (allowedSteps.Count - 1);
        }

        public bool IsStepAllowed(string previousStepKey, string nextStepKey)
        {
            var allowedSteps = GetAllowedSteps();
            var nextIndex = allowedSteps.FindIndex(x => string.Equals(nextStepKey, x, StringComparison.InvariantCultureIgnoreCase));

            if (IsOrderOptional)
                return nextIndex >= 0;

            var previousIndex = allowedSteps.FindIndex(x => string.Equals(previousStepKey, x, StringComparison.InvariantCultureIgnoreCase));
            if (previousIndex >= 0)
                return (nextIndex >= 0 && (nextIndex - 1) == previousIndex);

            return nextIndex == 0;
        }


        public List<string> GetAllowedSteps()
        {
            return JourneyEntryDefinitions
                .SelectMany(e => e.StepDefinitions)
                .Select(s => s.StepName)
                .ToList();
        }
    }
}