using System;
using Microsoft.AspNetCore.Hosting;
using Schroders.ServiceBase.Hosting;
using Topshelf.HostConfigurators;

namespace Schroders.ServiceDiscovery
{
    public class ServiceDiscovery : HostableService
    {
        public ServiceDiscovery(HostingConfiguration config, string contentRoot) : base(config, contentRoot)
        {
        }

        public override void UpdateHostConfigurator(HostConfigurator cfg)
        {
            throw new NotImplementedException();
        }

        protected override IWebHost CreateHost(HostingConfiguration config, string contentRoot)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseUrls(config.BaseUrl)
                .UseContentRoot(contentRoot)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }
    }
}
