
using Microsoft.Extensions.Options;
using Schroders.ServiceBase.RestClient;
using Schroders.ServiceDiscovery.RestClient.Configurations;

namespace Schroders.ServiceDiscovery.RestClient
{
    public class ServiceDiscoveryClient : IServiceDiscoveryClient
    {
        private readonly ServiceBase.RestClient.RestClient restClient;
        public ServiceDiscoveryClient(IOptions<ServiceDiscoveryConfiguration> settings)
        {
            restClient = new ServiceBase.RestClient.RestClient();
            restClient.BaseUrl = settings.Value.ServiceUrl;
        }

        public string GetServiceUrl(string serviceName, string serviceVersion, string token)
        {
            var serviceDiscoveryRequest = new RestRequest()
                .AddToken(token)
                .AddUrlSegment(serviceVersion)
                .AddUrlSegment(serviceName);

            var serviceDiscoveryResponse = restClient.Get<GetServiceUrlResponse>(serviceDiscoveryRequest).Result;
            return serviceDiscoveryResponse?.Content?.ServiceUrl;
        }
    }
}

