using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KahunaSluice.Core
{
  public class ConsumerService : BackgroundService
  {
    private readonly IConsumerMethodProvider _consumerMethodProvider;
    private readonly IConsumerProvider _consumerProvider;
    private readonly ILogger<ConsumerService> _logger;

    public ConsumerService(
      IConsumerMethodProvider consumerMethodProvider,
      IConsumerProvider consumerProvider,
      ILogger<ConsumerService> logger
      )
    {
      _consumerMethodProvider = consumerMethodProvider;
      _consumerProvider = consumerProvider;
      _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var methods = _consumerMethodProvider.GetConsumerMethods();
      var consumer = _consumerProvider.CreateConsumer<string, string>();

      consumer.Subscribe("my-topic");

      try
      {
        while (!stoppingToken.IsCancellationRequested)
        {
          try
          {
            var consumeResult = consumer.Consume(stoppingToken);
            _logger.LogInformation($"Received message at {consumeResult.TopicPartitionOffset}: ${consumeResult.Value}");
            foreach (var method in methods)
            {
              method.Invoke(default, new[] { consumeResult });
            }
          }
          catch (ConsumeException e)
          {
            _logger.LogError($"Consume error: {e.Error.Reason}");
          }
        }
      }
      catch (OperationCanceledException)
      {
        _logger.LogInformation("Closing consumer.");
        consumer.Close();
      }
      return Task.CompletedTask;
    }
  }
}
