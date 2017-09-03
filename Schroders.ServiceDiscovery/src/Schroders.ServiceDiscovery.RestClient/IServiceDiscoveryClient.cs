namespace Schroders.ServiceDiscovery.RestClient
{
    public interface IServiceDiscoveryClient
    {
        string GetServiceUrl(string serviceName, string serviceVersion, string token);
    }
}