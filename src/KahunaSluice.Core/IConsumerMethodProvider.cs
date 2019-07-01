using System.Collections.Generic;
using System.Reflection;

namespace KahunaSluice.Core
{
  public interface IConsumerMethodProvider
  {
    IEnumerable<MethodInfo> GetConsumerMethods();
  }
}
