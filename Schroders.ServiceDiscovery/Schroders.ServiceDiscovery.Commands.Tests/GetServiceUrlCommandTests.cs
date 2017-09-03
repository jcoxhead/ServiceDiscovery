using System.Collections.Generic;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Schroders.ServiceDiscovery.DataContracts.Dto;
using Schroders.ServiceDiscovery.DataContracts.Requests;
using Schroders.ServiceDiscovery.DataContracts.Responses;
using Schroders.ServiceDiscovery.Repository;
using Schroders.Test.Infrastructure;

namespace Schroders.ServiceDiscovery.Commands.Tests
{
    [TestClass]
    public class GetServiceUrlCommandTests : TestBase
    {
        [TestMethod]
        public void Execute_ShouldReturnUrlOfService()
        {
            const string testServiceName = "Test Service";
            const string version = "v1";
            const string url = "http://bogus/";

            var services = new List<ServiceConfigurationDto>
            {
                new ServiceConfigurationDto
                {
                    Name = testServiceName,
                    Url = url,
                    Version = version
                }
            };

            var repositoryResponse = new GetServicesResponse
            {
                Services = services
            };

            var repositoryMock = new Mock<IServiceRepository>();
            repositoryMock.Setup(x => x.GetServices()).Returns(repositoryResponse);

            ScopeAction action = scope =>
            {
                var request = new GetServiceUrlRequest() { ServiceName = testServiceName, ServiceVersion = version };

                var response = scope.Resolve<GetServiceUrlCommand>().Execute(request, new Dictionary<string, object>());

                Assert.IsNotNull(response);
                Assert.AreEqual(url, response.ServiceUrl);
            };

            RunAction(action, builder =>
            {
                builder.RegisterInstance(repositoryMock.Object).As<IServiceRepository>();

                builder.RegisterType<GetServiceUrlCommand>()
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .PropertiesAutowired();
            });
        }
    }
}
