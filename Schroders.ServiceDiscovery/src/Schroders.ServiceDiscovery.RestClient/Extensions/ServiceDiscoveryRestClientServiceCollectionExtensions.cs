
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schroders.ServiceDiscovery.RestClient.Configurations;

namespace Schroders.ServiceDiscovery.RestClient.Extensions
{
    public static class ServiceDiscoveryRestClientServiceCollectionExtensions
    {
        public static void AddServiceDiscoveryRestClient(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<IServiceDiscoveryClient, ServiceDiscoveryClient>();

            var section = configuration.GetSection(nameof(ServiceDiscoveryConfiguration));
            services.Configure<ServiceDiscoveryConfiguration>(section);
        }
    }
}