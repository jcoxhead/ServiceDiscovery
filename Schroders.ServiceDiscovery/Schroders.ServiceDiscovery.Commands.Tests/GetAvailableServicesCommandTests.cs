
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
    public class GetAvailableServicesCommandTests : TestBase
    {
        [TestMethod]
        public void Execute_ShouldReturnListOfAvailableServices()
        {
            const string testServiceName = "test service";

            var services = new List<ServiceConfigurationDto>
            {
                new ServiceConfigurationDto
                {
                    Name = testServiceName,
                    Url = "http://bogus/",
                    Version = "v1"
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
                var request = new GetAvailableServicesRequest();

                var commandContext = scope.Resolve<GetAvailableServicesCommand>();
                var response = commandContext.Execute(request);

                Assert.IsNotNull(response);
                Assert.AreEqual(response.Services[0].Name, testServiceName);
            };

            RunAction(action, builder =>
            {
                builder.RegisterInstance(repositoryMock.Object).As<IServiceRepository>();

                builder.RegisterType<GetAvailableServicesCommand>()
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .PropertiesAutowired();
            });
        }
    }
}