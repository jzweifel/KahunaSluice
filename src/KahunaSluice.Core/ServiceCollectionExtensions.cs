using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KahunaSluice.Core
{
  public static class ServiceCollectionExtensions
  {
    public static void AddKahunaSluice(this IServiceCollection serviceCollection)
    {
      if (serviceCollection == null)
        throw new ArgumentNullException(nameof(serviceCollection));

      serviceCollection.AddSingleton<IHostedService, ConsumerService>();
      serviceCollection.AddSingleton<IConsumerMethodProvider, ConsumerMethodProvider>();
    }
  }
}
