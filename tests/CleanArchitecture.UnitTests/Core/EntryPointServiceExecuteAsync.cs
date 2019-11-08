using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.UnitTests
{
    public class EntryPointServiceExecuteAsync
    {
        private static (EntryPointService, Mock<ILoggerAdapter<EntryPointService>>, Mock<IQueueReceiver>, Mock<IServiceLocator>) Factory()
        {
            var logger = new Mock<ILoggerAdapter<EntryPointService>>();
            var settings = new EntryPointSettings
            {
                ReceivingQueueName = "testQueue"
            };
            var queueReceiver = new Mock<IQueueReceiver>();
            var serviceLocator = new Mock<IServiceLocator>();

            var serviceProvider = SetupCreateScope(serviceLocator);

            // GetRequiredService is an extension method, I don't know how to continue
            serviceProvider.Setup(sp => sp.GetRequiredService(typeof(IRepository)))
                .Returns(new Mock<IRepository>());

            var service = new EntryPointService(logger.Object, settings, queueReceiver.Object, null, serviceLocator.Object, null);
            return (service, logger, queueReceiver, serviceLocator);
        }

        private static Mock<IServiceProvider> SetupCreateScope(Mock<IServiceLocator> serviceLocator)
        {
            Mock<IServiceScope> scope = new Mock<IServiceScope>();
            serviceLocator.Setup(sl => sl.CreateScope())
                            .Returns(scope.Object);
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            scope.Setup(s => s.ServiceProvider)
                .Returns(serviceProvider.Object);

            return serviceProvider;
        }

        [Fact]
        public async Task LogsExceptionsEncountered()
        {
            var (service, logger, _, _) = Factory();

            await service.ExecuteAsync();

            logger.Verify(l => l.LogError(It.IsAny<NullReferenceException>(), It.IsAny<string>()), Times.Once);
        }
    
        [Fact]
        public async Task MessageWasRetrievedFromTheQueue()
        {
            // example of getting inside of the CreateScope
            var (service, _, queueReceiver, _) = Factory();

            await service.ExecuteAsync();

            queueReceiver.Verify(qr => qr.GetMessageFromQueue("testQueue"), Times.Once);
        }
    }
}
