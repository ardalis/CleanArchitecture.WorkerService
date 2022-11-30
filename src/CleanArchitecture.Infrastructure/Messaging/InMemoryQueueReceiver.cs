using Ardalis.GuardClauses;
using CleanArchitecture.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Messaging;

/// <summary>
/// A simple implementation using the built-in Queue type and a single static instance.
/// </summary>
public class InMemoryQueueReceiver : IQueueReceiver
{
  public static Queue<string> MessageQueue = new Queue<string>();

  public async Task<string> GetMessageFromQueue(string queueName)
  {
    await Task.CompletedTask; // just so async is allowed
    Guard.Against.NullOrWhiteSpace(queueName, nameof(queueName));
    if (MessageQueue.Count == 0) return null;
    return MessageQueue.Dequeue();
  }
}
