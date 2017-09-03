using System;
using Microsoft.Extensions.Configuration;
using Schroders.ServiceBase.Hosting;

namespace Schroders.ServiceDiscovery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var contentRoot = AppDomain.CurrentDomain.BaseDirectory;

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("hosting.json", optional: false)
                .Build();

            var config = configBuilder.Get<HostingConfiguration>();

            HostingFacade.Run(() => new ServiceDiscovery(config, contentRoot));
        }
    }
}