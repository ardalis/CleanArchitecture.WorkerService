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
        private static (EntryPointService, Mock<ILoggerAdapter<EntryPointService>>, Mock<IQueueReceiver>, Mock<IServiceLocator>, Mock<IRepository>) Factory()
        {
            var logger = new Mock<ILoggerAdapter<EntryPointService>>();
            var settings = new EntryPointSettings
            {
                ReceivingQueueName = "testQueue"
            };
            var queueReceiver = new Mock<IQueueReceiver>();
            var serviceLocator = new Mock<IServiceLocator>();

            // maybe a tuple later on
            var repository = SetupCreateScope(serviceLocator);

            var service = new EntryPointService(logger.Object, settings, queueReceiver.Object, null, serviceLocator.Object, null);
            return (service, logger, queueReceiver, serviceLocator, repository);
        }

        private static Mock<IRepository> SetupCreateScope(Mock<IServiceLocator> serviceLocator)
        {
            var fakeScope = new Mock<IServiceScope>();
            serviceLocator.Setup(sl => sl.CreateScope())
                            .Returns(fakeScope.Object);

            var serviceProvider = new Mock<IServiceProvider>();
            fakeScope.Setup(s => s.ServiceProvider)
                .Returns(serviceProvider.Object);

            return SetupCustomInjection(serviceProvider);
        }

        private static Mock<IRepository> SetupCustomInjection(Mock<IServiceProvider> serviceProvider)
        {
            // GetRequiredService is an extension method, but GetService is not
            var repository = new Mock<IRepository>();
            serviceProvider.Setup(sp => sp.GetService(typeof(IRepository)))
                .Returns(repository.Object);

            // return a tuple as you have more dependencies
            return repository;
        }

        [Fact]
        public async Task LogsExceptionsEncountered()
        {
            var (service, logger, queueReceiver, _, _) = Factory();
            queueReceiver.Setup(qr => qr.GetMessageFromQueue(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Boom!"));

            await service.ExecuteAsync();

            logger.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task MessageWasRetrievedFromTheQueue()
        {
            // example of getting inside of the CreateScope
            var (service, _, queueReceiver, _, _) = Factory();

            await service.ExecuteAsync();

            queueReceiver.Verify(qr => qr.GetMessageFromQueue("testQueue"), Times.Once);
        }

        [Fact]
        public async Task MessageWasRetrievedFromTheQueue_WorksManyTimes()
        {
            // simulate multiple runs, but doesn't actually make the disposed object exception happen.
            // avoid {"Cannot access a disposed object.\r\nObject name: 'IServiceProvider'."}
            var (service, _, _, _, _) = Factory();
            await service.ExecuteAsync();
            await service.ExecuteAsync();
            await service.ExecuteAsync();
            await service.ExecuteAsync();
            var (service2, _, _, _, _) = Factory();
            await service2.ExecuteAsync();
            var (service3, _, _, _, _) = Factory();
            await service3.ExecuteAsync();

            Assert.True(true);
        }
    }
}
