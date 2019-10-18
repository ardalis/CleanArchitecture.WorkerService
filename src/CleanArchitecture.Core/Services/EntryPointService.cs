using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
    public class EntryPointService : IEntryPointService
    {
        private readonly ILoggerAdapter<EntryPointService> _logger;
        private readonly EntryPointSettings _settings;
        private readonly IQueueReceiver _queueReceiver;
        private readonly IQueueSender _queueSender;

        public EntryPointService(ILoggerAdapter<EntryPointService> logger,
            EntryPointSettings settings,
            IQueueReceiver queueReceiver,
            IQueueSender queueSender)
        {
            _logger = logger;
            _settings = settings;
            _queueReceiver = queueReceiver;
            _queueSender = queueSender;
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("{service} running at: {time}", nameof(EntryPointService), DateTimeOffset.Now);
            try
            {
                // read from the queue
                await _queueReceiver.GetMessageFromQueue(_settings.ReceivingQueueName);

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
