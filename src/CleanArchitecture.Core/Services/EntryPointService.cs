using CleanArchitecture.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
    public class EntryPointService : IEntryPointService
    {
        private readonly ILoggerAdapter<EntryPointService> _logger;
        private readonly IQueueReceiver _queueReceiver;
        private readonly IQueueSender _queueSender;

        public EntryPointService(ILoggerAdapter<EntryPointService> logger,
            IQueueReceiver queueReceiver,
            IQueueSender queueSender)
        {
            _logger = logger;
            _queueReceiver = queueReceiver;
            _queueSender = queueSender;
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("{service} running at: {time}", nameof(EntryPointService), DateTimeOffset.Now);
            try
            {
                // read from the queue
                await _queueReceiver.GetMessageFromQueue("");
                // do some work
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EntryPointService)}.{nameof(ExecuteAsync)} threw an exception.");
                // TODO: Decide if you want to re-throw which will crash the worker service
                //throw;
            }
        }
    }
}
