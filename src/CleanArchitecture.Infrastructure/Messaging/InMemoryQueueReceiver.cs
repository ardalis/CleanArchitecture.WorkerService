using Ardalis.GuardClauses;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Messaging
{
    public class InMemoryQueueReceiver : IQueueReceiver
    {
        public static Queue<string> MessageQueue = new Queue<string>();
        
        public async Task<string> GetMessageFromQueue(string queueName)
        {
            Guard.Against.NullOrWhiteSpace(queueName, nameof(queueName));
            if (MessageQueue.Count == 0) return null;
            return MessageQueue.Dequeue();
        }
    }
}
