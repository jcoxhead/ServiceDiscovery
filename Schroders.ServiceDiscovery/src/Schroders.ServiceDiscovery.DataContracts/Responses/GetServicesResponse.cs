using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schroders.ServiceDiscovery.DataContracts.Dto;

namespace Schroders.ServiceDiscovery.DataContracts.Responses
{
    public class GetServicesResponse
    {
        public List<ServiceConfigurationDto> Services { get; set; }
    }
}