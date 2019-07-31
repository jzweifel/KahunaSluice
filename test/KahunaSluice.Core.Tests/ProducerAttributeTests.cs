using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace KahunaSluice.Core
{
  public class ProducerAttributeTests
  {
    [Fact]
    public void ProducerAttribute_HasAttributeUsageAttribute()
    {
      var usageAttributes = (IList<AttributeUsageAttribute>)typeof(ProducerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
      usageAttributes.Should().NotBeEmpty();
    }

    [Fact]
    public void ProducerAttribute_OnlyValidOnMethods()
    {
      var usageAttribute = ((IList<AttributeUsageAttribute>)typeof(ProducerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)).First();
      usageAttribute.ValidOn.Should().Be(AttributeTargets.Method);
    }

    [Fact]
    public void ProducerAttribute_OnlyOneAllowed()
    {
      var usageAttribute = ((IList<AttributeUsageAttribute>)typeof(ProducerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)).First();
      usageAttribute.AllowMultiple.Should().BeFalse();
    }

    [Fact]
    public void ProducerAttribute_IsNotInheritable()
    {
      var usageAttribute = ((IList<AttributeUsageAttribute>)typeof(ProducerAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)).First();
      usageAttribute.Inherited.Should().BeFalse();
    }

    [Fact]
    public void ProducerAttribute_HasTopicName()
    {
      var attribute = new ProducerAttribute("my-cool-topic");
      attribute.TopicName.Should().Be("my-cool-topic");
    }

    [Fact]
    public void ProducerAttribute_HasPollInterval()
    {
      var attribute = new ProducerAttribute(default, 1);
      attribute.PollInterval.Should().Be(1);
    }
  }
}
