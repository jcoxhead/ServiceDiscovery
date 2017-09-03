using System;
using System.Collections.Generic;
using System.Linq;
using Schroders.ServiceBase.Commands;
using Schroders.ServiceDiscovery.DataContracts.Requests;
using Schroders.ServiceDiscovery.DataContracts.Responses;
using Schroders.ServiceDiscovery.Repository;

namespace Schroders.ServiceDiscovery.Commands
{
    public class GetServiceUrlCommand : ICommand<GetServiceUrlRequest, GetServiceUrlResponse>
    {
        private readonly IServiceRepository serviceRepository;

        public GetServiceUrlCommand(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public GetServiceUrlResponse Execute(GetServiceUrlRequest request, IDictionary<string, object> context)
        {
            var serviceResponse = serviceRepository.GetServices();

            if (serviceResponse == null || !serviceResponse.Services.Any())
            {
                return null;
            }

            var foundServices = serviceResponse.Services.Where(x => string.Compare(x.Name, request.ServiceName, StringComparison.OrdinalIgnoreCase) == 0
                && (string.IsNullOrEmpty(request.ServiceVersion) || string.Compare(x.Version, request.ServiceVersion, StringComparison.OrdinalIgnoreCase) == 0))
                .ToList();

            if (!foundServices.Any())
            {
                return null;
            }

            var result = new GetServiceUrlResponse
            {
                ServiceUrl = foundServices[0].Url
            };

            return result;
        }
     
    }
}
