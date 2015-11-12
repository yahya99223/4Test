using System;
using Core.DomainModel;

namespace Core.ServicesContracts
{
    public interface IJourneyService
    {
        void Start(Guid journeyDefinitionId);
        void Proceed(Guid journeyId, JourneyInputRequest request);
        void Cancel(Guid journeyId);
    }
}