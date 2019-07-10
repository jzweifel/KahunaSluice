using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KahunaSluice.Core
{
  public class ConsumerMethodProvider : IConsumerMethodProvider
  {
    private readonly Assembly[] _assemblies;

    public ConsumerMethodProvider(Assembly[] assemblies)
    {
      _assemblies = assemblies;
    }

    public ConsumerMethodProvider()
    {
      _assemblies = AppDomain.CurrentDomain.GetAssemblies();
    }

    public IEnumerable<IGrouping<string, ConsumerMethod>> GetConsumerMethods()
    {
      var types = _assemblies.SelectMany(a => a.GetTypes());
      var methods = types.SelectMany(t => t.GetMethods());
      var annotatedMethods = methods.Where(m => m.GetCustomAttributes(typeof(ConsumerAttribute), false).Length > 0);

      var consumerMethods = annotatedMethods.Select(m => new ConsumerMethod(
        topicName: ((ConsumerAttribute)m.GetCustomAttributes(typeof(ConsumerAttribute), false).Single()).TopicName,
        method: m
      ));

      var topicConsumerMethods = consumerMethods.GroupBy(cm => cm.TopicName);

      return topicConsumerMethods;

    }
  }
}
