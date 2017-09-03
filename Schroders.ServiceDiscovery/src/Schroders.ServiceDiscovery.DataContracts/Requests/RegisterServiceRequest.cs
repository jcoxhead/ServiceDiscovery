using System;

namespace Schroders.ServiceDiscovery.DataContracts.Requests
{
    public class RegisterServiceRequest
    {
        public string ServiceName { get; set; }

        public string ServiceVersion { get; set; }

        public string ServiceUrl { get; set; }
    }
}
