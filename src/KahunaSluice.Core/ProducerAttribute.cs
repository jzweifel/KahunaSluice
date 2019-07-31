using System;

namespace KahunaSluice.Core
{
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  public sealed class ProducerAttribute : Attribute
  {

    public string TopicName { get; }
    public int PollInterval { get; }
    public ProducerAttribute(string topic, int pollInterval = default)
    {
      TopicName = topic;
      PollInterval = pollInterval;
    }
  }
}
