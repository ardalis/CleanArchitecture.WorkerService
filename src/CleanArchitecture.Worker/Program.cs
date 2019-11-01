using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Core.Settings;

namespace CleanArchitecture.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();

            // seed some queue messages
            var queueSender = (IQueueSender)hostBuilder.Services.GetRequiredService(typeof(IQueueSender));
            for (int i = 0; i < 10; i++)
            {
                queueSender.SendMessageToQueue("https://google.com", "urlcheck");
            }

            hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
                    services.AddSingleton<IEntryPointService, EntryPointService>();

                    // Infrastructure.ContainerSetup
                    services.AddMessageQueues();
                    services.AddDbContext(hostContext.Configuration); 
                    services.AddRepositories();
                    services.AddUrlCheckingServices();

                    var workerSettings = new WorkerSettings();
                    hostContext.Configuration.Bind(nameof(WorkerSettings), workerSettings);
                    services.AddSingleton(workerSettings);

                    var entryPointSettings = new EntryPointSettings();
                    hostContext.Configuration.Bind(nameof(EntryPointSettings), entryPointSettings);
                    services.AddSingleton(entryPointSettings);

                    services.AddHostedService<Worker>();
                });
    }
}
