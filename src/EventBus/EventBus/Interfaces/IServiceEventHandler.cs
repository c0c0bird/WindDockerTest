using WindTest.EventBus.Events;
using System.Threading.Tasks;

namespace WindTest.EventBus.Abstractions
{
    public interface IServiceEventHandler<in TEvent> : IServiceEventHandler
        where TEvent : ServiceEvent
    {
        Task Handle(TEvent myEvent);
    }

    public interface IServiceEventHandler
    {
    }
}