using System;
using System.Reflection;
using System.Threading;
using FluentAssertions;
using Moq;
using Xunit;

namespace KahunaSluice.Core
{
  public class ConsumerServiceTests
  {
    Mock<IConsumerMethodProvider> _mockProvider = new Mock<IConsumerMethodProvider>();

    [Fact]
    public void ConsumerService_IsDisposable()
    {
      var service = new ConsumerService(_mockProvider.Object);

      Action act = () => service.Dispose();

      act.Should().NotThrow();
    }

    [Fact]
    public void ConsumerService_ExecuteAsync_InvokesMethodsFromProvider()
    {
      var mockMethodInfo = new Mock<MethodInfo>();

      _mockProvider.Setup(p => p.GetConsumerMethods()).Returns(new[] { mockMethodInfo.Object });
      var service = new ConsumerService(_mockProvider.Object);
      service.StartAsync(CancellationToken.None);

      mockMethodInfo.Verify(mi => mi.Invoke(default, default, default, default, default));
    }
  }
}
