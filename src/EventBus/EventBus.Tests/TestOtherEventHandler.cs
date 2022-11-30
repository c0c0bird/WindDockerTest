using WindTest.EventBus.Abstractions;
using System.Threading.Tasks;

namespace EventBus.Tests
{
    public class TestOtherEventHandler : IServiceEventHandler<TestEvent>
    {
        public bool Handled { get; private set; }

        public TestOtherEventHandler()
        {
            Handled = false;
        }

        public async Task Handle(TestEvent @event)
        {
            Handled = true;
        }
    }
}
