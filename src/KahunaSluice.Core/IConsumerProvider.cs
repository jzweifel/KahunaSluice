using Confluent.Kafka;

namespace KahunaSluice.Core
{
  public interface IConsumerProvider
  {
    IConsumer<TKey, TValue> CreateConsumer<TKey, TValue>();
  }
}
