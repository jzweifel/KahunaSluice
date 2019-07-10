using System;
using System.Threading;
using Confluent.Kafka;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KahunaSluice.Core
{
  public class ConsumerServiceTests
  {
    Mock<IConsumerMethodProvider> _mockConsumerMethodProvider = new Mock<IConsumerMethodProvider>();
    Mock<IConsumerProvider> _mockConsumerProvider = new Mock<IConsumerProvider>();
    Mock<ILogger<ConsumerService>> _mockLogger = new Mock<ILogger<ConsumerService>>();

    public ConsumerServiceTests()
    {
      var mockConsumer = new Mock<IConsumer<string, string>>();
      var value = new ConsumeResult<string, string>();
      mockConsumer.Setup(c => c.Consume(It.IsAny<CancellationToken>())).Returns(value);
      _mockConsumerProvider.Setup(cp => cp.CreateConsumer<string, string>()).Returns(mockConsumer.Object);
    }

    [Fact]
    public void ConsumerService_IsDisposable()
    {
      var service = new ConsumerService(_mockConsumerMethodProvider.Object, _mockConsumerProvider.Object, _mockLogger.Object);

      Action act = () => service.Dispose();

      act.Should().NotThrow();
    }
  }
}
