using System;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KahunaSluice.Core
{
  public static class ServiceCollectionExtensions
  {
    public static void AddKahunaSluice(this IServiceCollection serviceCollection, Action<ConsumerConfig> configure)
    {
      if (serviceCollection == null)
        throw new ArgumentNullException(nameof(serviceCollection));

      if (configure == null)
        throw new ArgumentNullException(nameof(configure));

      var config = new ConsumerConfig();

      configure(config);

      serviceCollection.AddSingleton<IHostedService, ConsumerService>();
      serviceCollection.AddSingleton<IConsumerMethodProvider, ConsumerMethodProvider>();
      serviceCollection.AddSingleton<IConsumerProvider, ConsumerProvider>();
      serviceCollection.AddSingleton<ConsumerConfig>(config);
    }
  }
}
