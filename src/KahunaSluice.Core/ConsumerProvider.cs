using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace KahunaSluice.Core
{
  public class ConsumerProvider : IConsumerProvider
  {

    private ConsumerConfig _config;
    private ILogger<ConsumerProvider> _logger;

    public ConsumerProvider(
      ConsumerConfig config,
      ILogger<ConsumerProvider> logger
    )
    {
      _config = config;
      _logger = logger;
    }

    public IConsumer<TKey, TValue> CreateConsumer<TKey, TValue>()
    {
      // Suppress Eof messages for now, no matter what the users of our
      // library supply for this value.
      // TODO: actually handle the potential presence of Eof messages.
      _config.EnablePartitionEof = false;

      var consumer = new ConsumerBuilder<TKey, TValue>(_config)
      .SetErrorHandler((_, e) => _logger.LogError(e.Reason))
      .SetStatisticsHandler((_, statsJson) => _logger.LogInformation(statsJson))
      .SetPartitionsAssignedHandler((c, partitions) => _logger.LogInformation($"Assigned partitions: [{string.Join(", ", partitions)}]"))
      .SetPartitionsRevokedHandler((c, partitions) => _logger.LogInformation($"Revoking assignment: [{string.Join(", ", partitions)}]"))
      .Build();

      return consumer;
    }
  }
}
