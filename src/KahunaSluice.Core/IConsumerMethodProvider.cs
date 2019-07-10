using System.Collections.Generic;
using System.Linq;

namespace KahunaSluice.Core
{
  public interface IConsumerMethodProvider
  {
    IEnumerable<IGrouping<string, ConsumerMethod>> GetConsumerMethods();
  }
}
