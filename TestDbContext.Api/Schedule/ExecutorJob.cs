using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestDbContext.Api.Service;

namespace TestDbContext.Api.Schedule
{
    public class ExecutorJob : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExecutorJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Executing background task.");

            stoppingToken.Register(() =>
                Console.WriteLine($"background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    await ExecuteScopedProcessAsync(scope.ServiceProvider);
                }
            }
        }

        private async Task ExecuteScopedProcessAsync(IServiceProvider provider)
        {
            Console.WriteLine("Executing scoped process");
            var executorService = provider.GetRequiredService<IExecutorService>();
            var result = await executorService.HandleAllTestDataModelAsync();
            Console.WriteLine("Execution completed with status {0}", result);
        }
    }
}
