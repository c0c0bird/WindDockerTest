using WindTest.EventBus;
using System.Linq;
using Xunit;

namespace EventBus.Tests
{
    public class InMemory_SubscriptionManager_Tests
    {
        [Fact]
        public void After_Creation_Should_Be_Empty()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();
            Assert.True(manager.IsEmpty);
        }

        [Fact]
        public void After_One_Event_Subscription_Should_Contain_The_Event()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();
            manager.AddSubscription<TestEvent,TestEventHandler>();
            Assert.True(manager.HasSubscriptionsForEvent<TestEvent>());
        }

        [Fact]
        public void After_All_Subscriptions_Are_Deleted_Event_Should_No_Longer_Exists()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();
            manager.AddSubscription<TestEvent, TestEventHandler>();
            manager.RemoveSubscription<TestEvent, TestEventHandler>();
            Assert.False(manager.HasSubscriptionsForEvent<TestEvent>());
        }

        [Fact]
        public void Deleting_Last_Subscription_Should_Raise_On_Deleted_Event()
        {
            bool raised = false;
            var manager = new InMemoryEventBusSubscriptionsManager();
            manager.OnEventRemoved += (o, e) => raised = true;
            manager.AddSubscription<TestEvent, TestEventHandler>();
            manager.RemoveSubscription<TestEvent, TestEventHandler>();
            Assert.True(raised);
        }

        [Fact]
        public void Get_Handlers_For_Event_Should_Return_All_Handlers()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();
            manager.AddSubscription<TestEvent, TestEventHandler>();
            manager.AddSubscription<TestEvent, TestOtherEventHandler>();
            var handlers = manager.GetHandlersForEvent<TestEvent>();
            Assert.Equal(2, handlers.Count());
        }

    }
}
