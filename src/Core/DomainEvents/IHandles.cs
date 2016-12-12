using System.Threading.Tasks;


namespace Core
{
    public interface IHandles<in T> where T : IDomainEvent
    {
        void Handle(T args); 
        Task HandleAsync(T args); 
    }
}