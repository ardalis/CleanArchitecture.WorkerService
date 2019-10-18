using CleanArchitecture.Core.Interfaces;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Messaging
{
    public class InMemoryQueueReceiver : IQueueReceiver
    {
        public async Task<string> GetMessageFromQueue(string queueName)
        {
            return await Task.FromResult(string.Empty);
        }
    }
}
