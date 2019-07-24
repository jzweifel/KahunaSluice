# KahunaSluice

![logo](media/logo-trimmed.png)

---

KahunaSluice is an annotation-driven abstraction library over [Confluent.Kafka](https://github.com/confluentinc/confluent-kafka-dotnet) for .NET Core.

**Note: This library is _NOT_ ready for production usage at this point.**

[![NuGet Badge](https://buildstats.info/nuget/KahunaSluice.Core)](https://www.nuget.org/packages/KahunaSluice.Core/)

## Aim of this Library

Provide developers with an easy, annotation-driven abstraction over Kafka consumers and producers to make building .NET Core stream data processing applications as simple as possible.

Inspired by Spring Cloud Stream and other Java libraries that intend to ease development of stream processing applications.

## Referencing

KahunaSluice is distributed via NuGet. One package is currently provided:

* [KahunaSluice.Core](https://www.nuget.org/packages/KahunaSluice.Core) [_netstandard2.0_] - The core library.

To install KahunaSluice.Core from within Visual Studio, search for KahunaSluice.Core in the NuGet Package Manager UI, or run the following command in the Package Manager Console:

```powershell
Install-Package KahunaSluice.Core -Version 0.3.0
```

To add a reference to a dotnet core project, execute the following at the command line:

```shell
dotnet add package -v 0.3.0 KahunaSluice.Core
```

## Usage

Take a look in the [examples](./examples) directory for example usage.

### Basic Consumer Example

Note that at this time, the library requires that the method annotated with `[ConsumerAttribute]` **_MUST_** be `public static void` and accept a single `ConsumeResult<string, string>` parameter.

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using KahunaSluice.Core;
using Confluent.Kafka;

namespace HelloWorld
{
  class Program
  {
    public static async Task Main(string[] args)
    {
      var host = new HostBuilder().ConfigureServices(sc => sc.AddKahunaSluice((config) =>
      {
        config.GroupId = "hello-kahuna-group";
        config.BootstrapServers = "localhost:9092";
      })).Build();

      await host.RunAsync();
    }

    [ConsumerAttribute("my-topic")]
    public static void MyCoolMethod(ConsumeResult<string, string> message)
    {
      Console.WriteLine($"Received message on {message.Topic}[{message.Partition.Value}] @" +
      $" offset {message.Offset.Value}: Key: {message.Key}, Value: {message.Value}.");
    }
  }
}

```

## Progress Overview

* [x] Basic Consumer
* [ ] Basic Producer
* [ ] Basic Transformer

## Contributing

### Build

To build the library or any test or example project, run the following from within the relevant project directory:

```shell
dotnet restore
dotnet build
```

To run an example project, run the following from within the example's project directory:

```shell
dotnet run <args>
```

### Test

To run tests in any of the test projects, run the following from within the relevant project directory:

```shell
dotnet test
```

## Q&A

⚠️ How to set up a Kafka cluster to test against locally?

[kafka-waffle-stack](https://github.com/TribalScale/kafka-waffle-stack) provides an easy-to-use Docker Compose solution to get a cluster running in Docker.

The [Apache Kafka Quickstart](https://kafka.apache.org/quickstart) is a good way to get Kafka up-and-running locally as well.
