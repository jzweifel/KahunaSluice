using System;

namespace KahunaSluice.Core
{
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  public sealed class ConsumerAttribute : Attribute
  {
    public string TopicName { get; }

    public ConsumerAttribute(string topicName)
    {
      TopicName = topicName;
    }
  }
}
