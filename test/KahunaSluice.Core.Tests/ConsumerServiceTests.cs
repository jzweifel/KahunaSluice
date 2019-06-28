using System;
using FluentAssertions;
using Xunit;

namespace KahunaSluice.Core.Tests
{
  public class ConsumerServiceTests
  {
    [Fact]
    public void ConsumerService_IsDisposable()
    {
      var service = new ConsumerService();

      Action act = () => service.Dispose();

      act.Should().NotThrow();
    }
  }
}
