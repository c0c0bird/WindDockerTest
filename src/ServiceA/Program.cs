using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ServiceA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((builderContext, config) =>
                {
                    config
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .WriteTo.File("ServiceA.log")
                        .WriteTo.Console();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
