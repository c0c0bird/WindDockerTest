using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceA.Containers;
using ServiceA.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WindTest.EventBus.Abstractions;
using WindTest.EventBus.Events;

namespace WindTest
{
    public class WindBackgroundService : BackgroundService
    {
        private readonly ILogger<WindBackgroundService> _logger;
        private readonly WindServiceSettings _settings;
        private readonly IEventBus _eventBus;

        public WindBackgroundService(
            IOptions<WindServiceSettings> settings,
            IEventBus eventBus,
            ILogger<WindBackgroundService> logger)
        {
            _settings = settings.Value;
            _eventBus = eventBus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"WindService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($"WindService background task is stopping."));

            var cache = new List<Site>();
            var intervalStart = DateTime.Now;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"WindService task doing background work.");

                // collect
                var currentData = await GetWindData();
                cache.AddRange(currentData);

                // every 5 minuntes: aggregate and emit
                if((DateTime.Now - intervalStart).Minutes >= 5)
                {
                    // aggregate values of all sites
                    var overview = SiteOverviewHelper.Aggegrate(cache);

                    EmitWindEvent(overview);

                    // reset interval
                    intervalStart = DateTime.Now;
                    cache.Clear();
                }

                await Task.Delay(_settings.Interval, stoppingToken);
            }

            _logger.LogDebug($"WindService background task is stopping.");
        }

        private async Task<IEnumerable<Site>> GetWindData()
        {
            IEnumerable<Site> sites = new List<Site>();

            try
            {
                sites = await SiteApiClient.GetSiteDetailsAsync();
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "FATAL ERROR: Database connections could not be opened: {Message}", exception.Message);
            }

            return sites;
        }

        private void EmitWindEvent(List<SiteOverview> siteOverviews)
        {
            var windEvent = new WindEvent(siteOverviews);

            _logger.LogInformation("Publishing event: {WindEventId} - ({WindEvent})", windEvent.Id, windEvent);

            _eventBus.Publish(windEvent);
        }
    }
}