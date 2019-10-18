using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IQueueReceiver
    {
        Task<string> GetMessageFromQueue(string queueName);
    }
}
