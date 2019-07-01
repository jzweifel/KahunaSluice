using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace KahunaSluice.Core
{
  public class ConsumerService : BackgroundService
  {
    private readonly IConsumerMethodProvider _consumerMethodProvider;

    public ConsumerService(IConsumerMethodProvider consumerMethodProvider)
    {
      _consumerMethodProvider = consumerMethodProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var methods = _consumerMethodProvider.GetConsumerMethods();
      foreach (var method in methods)
      {
        method.Invoke(default, default);
      }
      return Task.CompletedTask;
    }
  }
}
