using System;
using System.Collections.Generic;
using System.Linq;
using Schroders.ServiceBase.Commands;
using Schroders.ServiceDiscovery.DataContracts.Requests;
using Schroders.ServiceDiscovery.DataContracts.Responses;
using Schroders.ServiceDiscovery.Repository;

namespace Schroders.ServiceDiscovery.Commands
{
    public class GetAvailableServicesCommand : ICommand<GetAvailableServicesRequest, GetServicesResponse>
    {
        private readonly IServiceRepository serviceRepository;

        public GetAvailableServicesCommand(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public GetServicesResponse Execute(GetAvailableServicesRequest request, IDictionary<string, object> context)
        {
            var response = serviceRepository.GetServices();

            if (response == null || !response.Services.Any())
            {
                return null;
            }

            return new GetServicesResponse { Services = response.Services };
        }      
    }
}
