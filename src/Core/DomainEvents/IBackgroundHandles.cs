using System.Threading.Tasks;

namespace Core
{
    public interface IBackgroundHandles<in T> where T : IDomainEvent
    {
        Task HandlerTask(T args);
    }
}