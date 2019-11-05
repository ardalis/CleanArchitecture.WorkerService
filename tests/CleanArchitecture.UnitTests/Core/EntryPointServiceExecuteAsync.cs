using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.UnitTests
{
    public class EntryPointServiceExecuteAsync
    {
        private static (EntryPointService, Mock<ILoggerAdapter<EntryPointService>>) Factory()
        {
            var logger = new Mock<ILoggerAdapter<EntryPointService>>();
            var service = new EntryPointService(logger.Object, null, null, null, null, null);
            return (service, logger);
        }

        [Fact]
        public async Task LogsExceptionsEncountered()
        {
            var (service, logger) = Factory();

            await service.ExecuteAsync();

            logger.Verify(l => l.LogError(It.IsAny<NullReferenceException>(), It.IsAny<string>()), Times.Once);
        }
    
        [Fact]
        public async Task RunCodeInsideOfCreateScope()
        {
            // example of getting inside of the CreateScope
            // you'll want to remove this test later
            var (service, logger) = Factory();

            await service.ExecuteAsync();

            // didn't blow up, the scope was created
        }
    }
}
