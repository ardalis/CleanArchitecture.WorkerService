using Ardalis.GuardClauses;
using CleanArchitecture.Core.Interfaces;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Messaging
{
    public class InMemoryQueueReceiver : IQueueReceiver
    {
        public async Task<string> GetMessageFromQueue(string queueName)
        {
            Guard.Against.NullOrWhiteSpace(queueName, nameof(queueName));
            return await Task.FromResult(string.Empty);
        }
    }
}
