using System.Collections.Generic;

namespace Core.DomainModel
{
    public class JourneyInputRequest
    {
        public string StepName { get; set; }
        public IList<CapturedMedia> CapturedMedia { get; set; }
    }
}