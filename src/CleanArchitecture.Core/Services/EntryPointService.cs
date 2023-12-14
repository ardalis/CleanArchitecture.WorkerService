using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services;

/// <summary>
/// An example service that performs business logic
/// </summary>
public class EntryPointService(ILoggerAdapter<EntryPointService> _logger,
    EntryPointSettings _settings,
    IQueueReceiver _queueReceiver,
    IServiceLocator _serviceScopeFactoryLocator,
    IUrlStatusChecker _urlStatusChecker) : IEntryPointService
{
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
      if (string.IsNullOrEmpty(message)) return;

      // check 1 URL in the message
      var statusHistory = await _urlStatusChecker.CheckUrlAsync(message, "");

      // record HTTP status / response time / maybe existence of keyword in database
      repository.Add(statusHistory);

      _logger.LogInformation(statusHistory.ToString());
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
