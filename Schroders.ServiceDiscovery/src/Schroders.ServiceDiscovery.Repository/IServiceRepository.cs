
using Schroders.ServiceDiscovery.DataContracts.Responses;

namespace Schroders.ServiceDiscovery.Repository
{
    public interface IServiceRepository
    {
        GetServicesResponse GetServices();
    }
}
