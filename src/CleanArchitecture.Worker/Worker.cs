using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILoggerAdapter<Worker> _logger;
        private readonly IEntryPointService _entryPointService;

        public Worker(ILoggerAdapter<Worker> logger,
            IEntryPointService entryPointService)
        {
            _logger = logger;
            _entryPointService = entryPointService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CleanArchitecture.Worker service starting at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                await _entryPointService.ExecuteAsync();
                await Task.Delay(1000, stoppingToken); // TODO: Move delay to appSettings
            }
            _logger.LogInformation("CleanArchitecture.Worker service stopping at: {time}", DateTimeOffset.Now);
        }
    }
}
