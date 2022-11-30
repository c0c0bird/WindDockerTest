using WindTest.EventBus.Abstractions;
using WindTest.EventBus.Events;
using static WindTest.EventBus.InMemoryEventBusSubscriptionsManager;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;

namespace WindTest.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(ServiceEvent @event);

        void Subscribe<T, TH>()
            where T : ServiceEvent
            where TH : IServiceEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IServiceEventHandler<T>
            where T : ServiceEvent;
    }
}