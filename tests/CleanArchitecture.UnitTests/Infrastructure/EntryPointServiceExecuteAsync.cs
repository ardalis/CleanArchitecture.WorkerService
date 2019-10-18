using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure.Messaging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.UnitTests
{
    public class InMemoryQueueReceiverGetMessageFromQueue
    {
        [Fact]
        public async Task ThrowsNullExceptionGivenNullQueuename()
        {
            var receiver = new InMemoryQueueReceiver();

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => receiver.GetMessageFromQueue(null));
        }

        [Fact]
        public async Task ThrowsArgumentExceptionGivenEmptyQueuename()
        {
            var receiver = new InMemoryQueueReceiver();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => receiver.GetMessageFromQueue(String.Empty));
        }
    }
}
