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
public class Worker : BackgroundService
{
  private readonly ILoggerAdapter<Worker> _logger;
  private readonly IEntryPointService _entryPointService;
  private readonly WorkerSettings _settings;

  public Worker(ILoggerAdapter<Worker> logger,
      IEntryPointService entryPointService,
      WorkerSettings settings)
  {
    _logger = logger;
    _entryPointService = entryPointService;
    _settings = settings;
  }

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
