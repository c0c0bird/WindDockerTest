using System;

namespace ServiceA.Containers
{
    public class Turbine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Version { get; set; }
        public string MaxProduction { get; set; }
        public double? CurrentProduction { get; set; }
        public double? WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public DateTime Time { get; set; }
    }
}