using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EntryPointService(ILoggerAdapter<EntryPointService> logger,
            EntryPointSettings settings,
            IQueueReceiver queueReceiver,
            IQueueSender queueSender,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _settings = settings;
            _queueReceiver = queueReceiver;
            _queueSender = queueSender;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("{service} running at: {time}", nameof(EntryPointService), DateTimeOffset.Now);
            try
            {
                // create a scope
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var repository =
                        scope.ServiceProvider
                            .GetRequiredService<IRepository>();

                // read from the queue
                await _queueReceiver.GetMessageFromQueue(_settings.ReceivingQueueName);

                // check 1 URL in the message

                var statusHistory = new UrlStatusHistory
                {
                    RequestId = "n/a",
                    StatusCode = 200,
                    Uri = "http://ardalis.com"
                };

                // record HTTP status / response time / maybe existence of keyword in database
                repository.Add(statusHistory);
                }
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
