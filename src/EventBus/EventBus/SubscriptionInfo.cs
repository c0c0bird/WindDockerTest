using WindTest.EventBus.Abstractions;
using WindTest.EventBus.Events;
using static WindTest.EventBus.InMemoryEventBusSubscriptionsManager;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using WindTest.EventBus.Interfaces;

namespace WindTest.EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        public class SubscriptionInfo
        {
            public bool IsDynamic { get; }
            public Type HandlerType { get; }

            private SubscriptionInfo(bool isDynamic, Type handlerType)
            {
                IsDynamic = isDynamic;
                HandlerType = handlerType;
            }

            public static SubscriptionInfo Dynamic(Type handlerType) =>
                new SubscriptionInfo(true, handlerType);

            public static SubscriptionInfo Typed(Type handlerType) =>
                new SubscriptionInfo(false, handlerType);
        }
    }
}