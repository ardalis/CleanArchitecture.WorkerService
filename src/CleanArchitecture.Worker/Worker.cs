using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Worker;

/// <summary>
/// The Worker is a BackgroundService that is executed periodically
/// It should not contain any business logic but should call an entrypoint service that
/// execute once per time period.
/// </summary>
public class Worker(ILoggerAdapter<Worker> _logger,
    IEntryPointService _entryPointService,
    WorkerSettings _settings) : BackgroundService
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("CleanArchitecture.Worker service starting at: {time}", DateTimeOffset.Now);
    while (!stoppingToken.IsCancellationRequested)
    {
      await _entryPointService.ExecuteAsync();
      await Task.Delay(_settings.DelayMilliseconds, stoppingToken);
    }
    _logger.LogInformation("CleanArchitecture.Worker service stopping at: {time}", DateTimeOffset.Now);
  }
}
