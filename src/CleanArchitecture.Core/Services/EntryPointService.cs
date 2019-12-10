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
        private readonly IServiceLocator _serviceScopeFactoryLocator;
        private readonly IUrlStatusChecker _urlStatusChecker;

        public EntryPointService(ILoggerAdapter<EntryPointService> logger,
            EntryPointSettings settings,
            IQueueReceiver queueReceiver,
            IQueueSender queueSender,
            IServiceLocator serviceScopeFactoryLocator,
            IUrlStatusChecker urlStatusChecker)
        {
            _logger = logger;
            _settings = settings;
            _queueReceiver = queueReceiver;
            _queueSender = queueSender;
            _serviceScopeFactoryLocator = serviceScopeFactoryLocator;
            _urlStatusChecker = urlStatusChecker;
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("{service} running at: {time}", nameof(EntryPointService), DateTimeOffset.Now);
            try
            {
                // EF Requires a scope so we are creating one per execution here
                using var scope = _serviceScopeFactoryLocator.CreateScope();
                    var repository =
                        scope.ServiceProvider
                            .GetService<IRepository>();

                // read from the queue
                string message = await _queueReceiver.GetMessageFromQueue(_settings.ReceivingQueueName);
                if (String.IsNullOrEmpty(message)) return;

                // check 1 URL in the message
                var statusHistory = await _urlStatusChecker.CheckUrlAsync(message, "");

                // record HTTP status / response time / maybe existence of keyword in database
                repository.Add(statusHistory);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EntryPointService)}.{nameof(ExecuteAsync)} threw an exception.");
                // TODO: Decide if you want to re-throw which will crash the worker service
                //throw;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
