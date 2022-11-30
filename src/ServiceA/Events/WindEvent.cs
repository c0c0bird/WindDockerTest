using System.Collections.Generic;
using ServiceA.Containers;

namespace WindTest.EventBus.Events
{
    public class WindEvent : ServiceEvent
    {
        public List<SiteOverview> Sites { get; }

        public WindEvent(List<SiteOverview> sites)
        {
            Sites = sites;
        }
    }
}