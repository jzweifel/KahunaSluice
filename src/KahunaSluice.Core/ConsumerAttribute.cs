using System;

namespace KahunaSluice.Core
{
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  public sealed class ConsumerAttribute : Attribute
  {
    private string _topicName;

    public ConsumerAttribute(string topicName)
    {
      _topicName = topicName;
    }

    public string TopicName
    {
      get { return _topicName; }
    }
  }
}
