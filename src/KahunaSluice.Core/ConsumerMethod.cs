using System.Reflection;

namespace KahunaSluice.Core
{
  public class ConsumerMethod
  {
    public string TopicName { get; }
    public MethodInfo Method { get; }

    public ConsumerMethod(string topicName, MethodInfo method)
    {
      TopicName = topicName;
      Method = method;
    }
  }
}
