using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWork6
{
    internal static class InjectionRoot
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static TRootStarter Build<TRootStarter>()
        {
            var services = new Startup(LoadConfigurations()).ConfigureServices();

            ServiceProvider = services.BuildServiceProvider(true);

            return ServiceProvider.GetRequiredService<TRootStarter>();
        }

        private static IConfigurationRoot LoadConfigurations()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, false)
                .Build();
        }
    }
}