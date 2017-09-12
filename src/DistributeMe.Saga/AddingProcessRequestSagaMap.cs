using MassTransit.EntityFrameworkIntegration;

namespace DistributeMe.Saga
{
    internal class AddingProcessRequestSagaMap : SagaClassMapping<AddingProcessRequestSagaState>
    {
        public AddingProcessRequestSagaMap()
        {
            Property(x => x.CurrentState).HasMaxLength(64);
            Property(x => x.FaceFinished);
            Property(x => x.OcrFinished);        
            Property(x => x.RequestId);
            Property(x => x.EndProcessDate);
            Property(x => x.StartProcessDate);
            Property(x => x.ExtractedText);
            Property(x => x.FacesCount);
            Property(x => x.RequestFinishedStatusBits);
        }
    }
}