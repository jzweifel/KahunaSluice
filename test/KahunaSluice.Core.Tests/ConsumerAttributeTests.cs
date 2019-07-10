using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace KahunaSluice.Core
{
  public class ConsumerAttributeTests
  {
    [Fact]
    public void ConsumerAttribute_HasAttributeUsageAttribute()
    {
      var usageAttributes = (IList<AttributeUsageAttribute>)typeof(ConsumerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
      usageAttributes.Should().NotBeEmpty();
    }

    [Fact]
    public void ConsumerAttribute_OnlyValidOnMethods()
    {
      var usageAttribute = ((IList<AttributeUsageAttribute>)typeof(ConsumerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)).First();
      usageAttribute.ValidOn.Should().Be(AttributeTargets.Method);
    }

    [Fact]
    public void ConsumerAttribute_OnlyOneAllowed()
    {
      var usageAttribute = ((IList<AttributeUsageAttribute>)typeof(ConsumerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)).First();
      usageAttribute.AllowMultiple.Should().BeFalse();
    }

    [Fact]
    public void ConsumerAttribute_IsNotInheritable()
    {
      var usageAttribute = ((IList<AttributeUsageAttribute>)typeof(ConsumerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)).First();
      usageAttribute.Inherited.Should().BeFalse();
    }

    [Fact]
    public void ConsumerAttribute_HasTopicName()
    {
      var attribute = new ConsumerAttribute("my-cool-topic");
      attribute.TopicName.Should().Be("my-cool-topic");
    }
  }
}
