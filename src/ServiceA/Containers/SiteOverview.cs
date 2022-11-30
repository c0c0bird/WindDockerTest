using System;
using System.Collections.Generic;

namespace ServiceA.Containers
{
    public class SiteOverview
    {
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public double? WindSpeed { get; set; }
        public double? CurrentProduction { get; set; }

        public IEnumerable<TurbineOverview> Turbines { get; set; }
    }
}