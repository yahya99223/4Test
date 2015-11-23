using System;
using System.Collections.Generic;
using System.Linq;
using Core.DomainModel;
using Core.ServicesContracts;

namespace Services.Default.Journey
{
    public class JourneyService : IJourneyService
    {
        private List<Core.DomainModel.Journey> inMemoryJourneyList;
        private List<JourneyDefinition> inMemoryJourneyDefinitionList;

        public JourneyService()
        {
            inMemoryJourneyList = new List<Core.DomainModel.Journey>();
            inMemoryJourneyDefinitionList = new List<JourneyDefinition>()
            {
                new JourneyDefinition(Guid.Empty, "First Path", false, new[]
                {
                    new JourneyEntryDefinition(Guid.Empty, 1, EntryType.IdentityCard, false, new[]
                    {
                        new EntryStepDefinition("1_IdentityCard_FrontSide", false),
                        new EntryStepDefinition("2_IdentityCard_BackSide", true),
                    }),
                }),
            };
        }

        public void Start(Guid journeyDefinitionId)
        {
            var journeydefinition = inMemoryJourneyDefinitionList.First();
            var journey = new Core.DomainModel.Journey(journeydefinition);
            journey.Start();
            inMemoryJourneyList.Add(journey);
        }


        public void Proceed(Guid journeyId, JourneyInputRequest request)
        {
            var journey = inMemoryJourneyList.First(j => j.Id == journeyId);
            journey.Proceed(request);
        }

        public void Cancel(Guid journeyId)
        {
            var journey = inMemoryJourneyList.First(j => j.Id == journeyId);
            journey.Cancel();
        }
    }
}
