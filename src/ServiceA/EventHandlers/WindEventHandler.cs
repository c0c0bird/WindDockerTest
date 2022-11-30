using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WindTest.EventBus.Abstractions;
using WindTest.EventBus.Events;

namespace ServiceA.Events
{
    public class WindEventHandler : IServiceEventHandler<WindEvent>
    {
        private readonly ILogger<WindEventHandler> _logger;

        public WindEventHandler(ILogger<WindEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(WindEvent myEvent)
        {
            _logger.LogInformation("Handling event: {ServiceEventId} at {AppName} - ({@ServiceEvent})", myEvent.Id, "InteractiveServices", myEvent);
        }
    }
}
