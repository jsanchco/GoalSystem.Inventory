using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Enumerations;
using GoalSystem.Inventory.Domain.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Api.HostedServices
{
    /// <summary>
    /// Class handler to check when any Item has expired
    /// </summary>
    public class CheckItemsExpiredHostedService : IHostedService, IDisposable
    {   
        /// <summary>
        /// Logger of App
        /// </summary>
        private readonly ILogger<CheckItemsExpiredHostedService> _logger;

        /// <summary>
        /// Repository where is stored the Items of Inventory
        /// </summary>
        private readonly IRepositoryItemInventory _repositoryItemInventory;

        /// <summary>
        /// Service in order to publish events
        /// </summary>
        private readonly IEventsService _eventsService;

        private int executionCount = 0;
        private Timer _timer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repositoryItemInventory"></param>
        /// <param name="eventsService"></param>
        public CheckItemsExpiredHostedService(
            ILogger<CheckItemsExpiredHostedService> logger,
            IRepositoryItemInventory repositoryItemInventory,
            IEventsService eventsService)
        {
            _logger = logger;
            _repositoryItemInventory = repositoryItemInventory;
            _eventsService = eventsService;
        }

        /// <summary>
        /// Start the service and repeat every 5 seconds
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CheckItemsExpiredHostedService running ...");

            _timer = new Timer(
                DoWork, 
                null, 
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Handler when fire every 5 seconds
        /// </summary>
        /// <param name="state"></param>
        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                $"CheckItemsExpiredHostedService is working [{count}]");

            var itemsInventryExpired = _repositoryItemInventory.GetExpired().Result;
            foreach (var itemInventryExpired in itemsInventryExpired)
            {
                _eventsService.Publish(new Event(TypeEvent.ExpiredItemInventory));
            }
        }

        /// <summary>
        /// Stop the hosted service
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CheckItemsExpiredHostedService is stopping");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Dispose all related with the hosted service
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
