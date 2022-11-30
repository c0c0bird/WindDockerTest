using System;
using System.Collections.Generic;
using System.Linq;
using ServiceA.Containers;

namespace ServiceA.Services
{
    public class SiteOverviewHelper
    {
        /// <summary>
        /// Gets the aggregated turbine data for every turbine in a site
        /// </summary>
        /// <param name="sites"></param>
        /// <returns></returns>
        public static List<SiteOverview> Aggegrate(IEnumerable<Site> sites)
        {
            var result = new List<SiteOverview>();

            // group sites by ID
            var siteGroups = sites.GroupBy(s => s.Id);

            foreach(var siteGroup in siteGroups)
            {
                //  group turbines by ID

                var turbineGroups = siteGroup.Select(s => s.Turbines)
                                        .Aggregate((a,b) => a.Concat(b))
                                        .GroupBy(t => t.Id);

                //  aggregate values for each turbine ID

                var turbineOverviews = turbineGroups.Select(g => new TurbineOverview
                {
                    Id = g.Key,
                    CurrentProduction = g.Select(t => t.CurrentProduction).Average(),
                    WindSpeed = g.Select(t => t.WindSpeed).Average()
                });

                var siteOverview = new SiteOverview
                {
                    Id = siteGroup.Key,
                    Time = DateTime.Now,
                    Turbines = turbineOverviews,
                    CurrentProduction = turbineOverviews.Select(t => t.CurrentProduction).Average(),
                    WindSpeed = turbineOverviews.Select(t => t.WindSpeed).Average()
                };

                result.Add(siteOverview);
            }

            return result;
        }
    }
}
