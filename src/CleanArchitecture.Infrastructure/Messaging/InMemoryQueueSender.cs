using CleanArchitecture.Core.Interfaces;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Messaging
{
    public class InMemoryQueueSender : IQueueSender
    {
        public async Task SendMessageToQueue(string message, string queueName)
        {
            InMemoryQueueReceiver.MessageQueue.Enqueue(message);
        }
    }
}
