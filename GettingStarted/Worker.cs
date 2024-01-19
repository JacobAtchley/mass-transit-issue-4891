using System;
using System.Threading;
using System.Threading.Tasks;
using GettingStarted.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tenSeconds = TimeSpan.FromSeconds(20);
            var cancellationToken = new CancellationTokenSource(tenSeconds).Token;
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var requestClient = _bus.CreateRequestClient<GettingStartedRequest>();
                    var request = new GettingStartedRequest { Request = $"Request at time{DateTimeOffset.Now}" };
                    var response = await requestClient.GetResponse<GettingStartedResponse>(request, cancellationToken, tenSeconds);

                    _logger.LogInformation("Response: {Response}", response.Message.Response);

                    await Task.Delay(1000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in worker");
                }
            }
        }
    }
}