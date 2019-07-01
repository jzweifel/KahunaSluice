using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KahunaSluice.Core
{
  public class ConsumerMethodProvider
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

    public IEnumerable<MethodInfo> GetConsumerMethods()
    {
      var types = _assemblies.SelectMany(a => a.GetTypes());
      var methods = types.SelectMany(t => t.GetMethods());
      var annotatedMethods = methods.Where(m => m.GetCustomAttributes(typeof(ConsumerAttribute), false).Length > 0);

      return annotatedMethods;

    }
  }
}
