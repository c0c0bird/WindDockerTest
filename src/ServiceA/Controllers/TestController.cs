using WindTest.EventBus.Abstractions;
using WindTest.EventBus.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceA.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindTest;
using ServiceA.Services;
using ServiceA.Containers;

namespace ServiceA.Controllers
{
    //[Authorize]
    //[AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        //private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<TestController> _logger;


        public TestController(ILogger<TestController> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IEnumerable<SiteOverview>> Get()
        {
            var sites = await SiteApiClient.GetSiteDetailsAsync();
            var overview = SiteOverviewHelper.Aggegrate(sites);
            var eventMessage = new WindEvent(overview);

            try
            {
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", eventMessage.Id, "InteractiveServices");
                throw;
            }

            return overview;
        }
    }
}
