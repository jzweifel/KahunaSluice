using System;
using Microsoft.Extensions.DependencyInjection;

namespace KahunaSluice.Core
{
  public static class ServiceCollectionExtensions
  {
    public static void AddKahunaSluice(this ServiceCollection serviceCollection)
    {
      if (serviceCollection == null)
        throw new ArgumentNullException(nameof(serviceCollection));

      serviceCollection.AddSingleton<IConsumerService, ConsumerService>();
    }
  }
}
