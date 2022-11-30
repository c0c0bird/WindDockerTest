using System;
using System.Collections.Generic;

namespace ServiceA.Containers
{
    public class Site
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime Time { get; set; }
        public IEnumerable<Turbine> Turbines { get; set; }
    }
}