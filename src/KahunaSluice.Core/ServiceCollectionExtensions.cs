using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KahunaSluice.Core
{
  public static class ServiceCollectionExtensions
  {
    public static void AddKahunaSluice(this ServiceCollection serviceCollection)
    {
      if (serviceCollection == null)
        throw new ArgumentNullException(nameof(serviceCollection));

      serviceCollection.AddSingleton<IHostedService, ConsumerService>();
    }
  }
}
