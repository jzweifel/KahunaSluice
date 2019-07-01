using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace KahunaSluice.Core
{
  public class ServiceCollectionExtensionsTests
  {
    [Fact]
    public void AddKahunaSluice_ServiceCollectionNull()
    {
      Action action = () => ServiceCollectionExtensions.AddKahunaSluice(null);

      action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("serviceCollection");
    }

    [Fact]
    public void AddKahunaSluice_RegistersConsumerServiceSingleton()
    {
      var services = new ServiceCollection();

      ServiceCollectionExtensions.AddKahunaSluice(services);

      ServiceDescriptor expected = new ServiceDescriptor(typeof(IHostedService), typeof(ConsumerService), default);

      services.Should().ContainEquivalentOf(expected);
    }



    [Fact]
    public void AddKahunaSluice_RegistersConsumerMethodProviderSingleton()
    {
      var services = new ServiceCollection();

      ServiceCollectionExtensions.AddKahunaSluice(services);

      ServiceDescriptor expected = new ServiceDescriptor(typeof(IConsumerMethodProvider), typeof(ConsumerMethodProvider), default);

      services.Should().ContainEquivalentOf(expected);
    }

    [Fact]
    public void AddKahunaSluice_ShouldBeExtensionMethod()
    {
      var services = new ServiceCollection();

      Action action = () => services.AddKahunaSluice();

      action.Should().NotThrow();
    }
  }
}
