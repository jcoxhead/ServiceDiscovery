using Microsoft.AspNetCore.Mvc;
using Schroders.ServiceBase.Commands.Pipeline.PipelineFactory;
using Schroders.ServiceDiscovery.DataContracts.Requests;
using Schroders.ServiceDiscovery.DataContracts.Responses;

namespace Schroders.ServiceDiscovery.Controllers
{
    [Route("service-discovery")]
    public class ServiceDiscoveryController : Controller
    {
        private readonly IPipelineFactory pipelineFactory;

        public ServiceDiscoveryController(IPipelineFactory pipelineFactory)
        {
            this.pipelineFactory = pipelineFactory;
        }

        [HttpGet]
        public GetServicesResponse GetAvailableServices()
        {
            var request = new GetAvailableServicesRequest();
            var result = pipelineFactory.Get<GetAvailableServicesRequest, GetServicesResponse>().Execute(request).Result;

            return result;
        }

        [HttpGet("{version:required}/{name:required}")]
        public GetServiceUrlResponse GetServiceUrl(string name, string version)
        {
            var request = new GetServiceUrlRequest
            {
                ServiceName = name,
                ServiceVersion = version
            };

            var result = pipelineFactory.Get<GetServiceUrlRequest, GetServiceUrlResponse>().Execute(request).Result;

            return result;
        }

        [HttpPost]
        public ActionResult Register([FromBody]RegisterServiceRequest request)
        {
            return StatusCode(501);
        }
    }
}