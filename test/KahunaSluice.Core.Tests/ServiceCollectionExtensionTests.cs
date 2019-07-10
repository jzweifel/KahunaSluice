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
      Action action = () => ServiceCollectionExtensions.AddKahunaSluice(null, (_) => { });

      action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("serviceCollection");
    }

    [Fact]
    public void AddKahunaSluice_ConfigureNull()
    {
      var services = new ServiceCollection();

      Action action = () => ServiceCollectionExtensions.AddKahunaSluice(services, null);

      action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("configure");
    }

    [Fact]
    public void AddKahunaSluice_RegistersConsumerServiceSingleton()
    {
      var services = new ServiceCollection();

      ServiceCollectionExtensions.AddKahunaSluice(services, (_) => { });

      ServiceDescriptor expected = new ServiceDescriptor(typeof(IHostedService), typeof(ConsumerService), default);

      services.Should().ContainEquivalentOf(expected);
    }

    [Fact]
    public void AddKahunaSluice_RegistersConsumerMethodProviderSingleton()
    {
      var services = new ServiceCollection();

      ServiceCollectionExtensions.AddKahunaSluice(services, (_) => { });

      ServiceDescriptor expected = new ServiceDescriptor(typeof(IConsumerMethodProvider), typeof(ConsumerMethodProvider), default);

      services.Should().ContainEquivalentOf(expected);
    }

    [Fact]
    public void AddKahunaSluice_RegistersConsumerProviderSingleton()
    {
      var services = new ServiceCollection();

      ServiceCollectionExtensions.AddKahunaSluice(services, (_) => { });

      ServiceDescriptor expected = new ServiceDescriptor(typeof(IConsumerProvider), typeof(ConsumerProvider), default);

      services.Should().ContainEquivalentOf(expected);
    }

    [Fact]
    public void AddKahunaSluice_ShouldBeExtensionMethod()
    {
      var services = new ServiceCollection();

      Action action = () => services.AddKahunaSluice((_) => { });

      action.Should().NotThrow();
    }
  }
}
