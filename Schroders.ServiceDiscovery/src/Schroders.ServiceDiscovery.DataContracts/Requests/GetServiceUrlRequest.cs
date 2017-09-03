namespace Schroders.ServiceDiscovery.DataContracts.Requests
{
    public class GetServiceUrlRequest
    {
        public string ServiceName { get; set; }

        public string ServiceVersion { get; set; }
    }
}