using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using KahunaSluice.Core;

namespace HelloWorld
{
  class Program
  {
    public static async Task Main(string[] args)
    {
      var host = new HostBuilder().ConfigureServices(sc => sc.AddKahunaSluice()).Build();

      await host.RunAsync();
    }

    [ConsumerAttribute]
    public static void MyCoolMethod()
    {
      System.Console.WriteLine("Hello World from KahunaSluice!");
    }
  }
}
