using Microsoft.Extensions.Options;
using Schroders.ServiceDiscovery.DataContracts.Configurations;
using Schroders.ServiceDiscovery.DataContracts.Responses;

namespace Schroders.ServiceDiscovery.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private ServicesConfiguration servicesConfiguration;

        public ServiceRepository(IOptions<ServicesConfiguration> options)
        {
            this.servicesConfiguration = options.Value;
        }

        public GetServicesResponse GetServices()
        {
            var response = new GetServicesResponse
            {
                Services = servicesConfiguration.Services
            };

            return response;
        }
    }
}