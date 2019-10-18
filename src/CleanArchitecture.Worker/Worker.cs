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

        public Worker(ILoggerAdapter<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
