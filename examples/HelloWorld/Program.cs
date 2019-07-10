using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using KahunaSluice.Core;
using Confluent.Kafka;

namespace HelloWorld
{
  class Program
  {
    public static async Task Main(string[] args)
    {
      var host = new HostBuilder().ConfigureServices(sc => sc.AddKahunaSluice((config) =>
      {
        config.GroupId = "hello-kahuna-group";
        config.BootstrapServers = "localhost:9092";
      })).Build();

      await host.RunAsync();
    }

    [ConsumerAttribute("my-topic")]
    public static void MyCoolMethod(ConsumeResult<string, string> message)
    {
      System.Console.WriteLine($"Received message on {message.Topic}[{message.Partition.Value}] @" +
      $" offset {message.Offset.Value}: Key: {message.Key}, Value: {message.Value}.");
    }
  }
}
