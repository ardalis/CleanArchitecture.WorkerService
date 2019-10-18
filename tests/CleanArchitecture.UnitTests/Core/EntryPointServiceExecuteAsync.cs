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
        [Fact]
        public async Task LogsExceptionsEncountered()
        {
            var logger = new Mock<ILoggerAdapter<EntryPointService>>();
            var service = new EntryPointService(logger.Object, null, null);

            await service.ExecuteAsync();

            logger.Verify(l => l.LogError(It.IsAny<NullReferenceException>(), It.IsAny<string>()), Times.Once);
        }
    }
}
