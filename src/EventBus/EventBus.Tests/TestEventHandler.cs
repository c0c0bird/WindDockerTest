using WindTest.EventBus.Abstractions;
using System.Threading.Tasks;

namespace EventBus.Tests
{
    public class TestEventHandler : IServiceEventHandler<TestEvent>
    {
        public bool Handled { get; private set; }

        public TestEventHandler()
        {
            Handled = false;
        }

        public async Task Handle(TestEvent @event)
        {
            Handled = true;
        }
    }
}
