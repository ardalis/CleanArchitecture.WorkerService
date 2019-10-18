using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IQueueSender
    {
        Task SendMessageToQueue(string message, string queueName);
    }
}
