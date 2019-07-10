using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using FluentAssertions;
using Xunit;

namespace KahunaSluice.Core
{
  public class ConsumerMethodProviderTests
  {
    [Fact]
    public void GetConsumerMethods_ReturnsEmpty_WhenNoAnnotatedMethods()
    {
      var assembly = CreateAssemblyWithSomeClassWithSomeMethod();

      var provider = new ConsumerMethodProvider(new[] { assembly });

      var methods = provider.GetConsumerMethods();

      methods.Should().BeEmpty();
    }

    [Fact]
    public void GetConsumerMethods_ReturnsOne_WhenAnnotationExists()
    {
      var assembly = CreateAssemblyWithSomeClassWithAnnotatedSomeMethod();

      var provider = new ConsumerMethodProvider(new[] { assembly });

      var methods = provider.GetConsumerMethods();

      methods.Should().ContainSingle();
    }

    [Fact]
    public void GetConsumerMethods_ContainsAnnotatedMethods()
    {
      var assembly = CreateAssemblyWithSomeClassWithAnnotatedSomeMethod();

      var provider = new ConsumerMethodProvider(new[] { assembly });

      var methods = provider.GetConsumerMethods();

      methods.Single(m => m.Key == "topic-name").Should().Contain(m => m.Method.Name == "SomeMethod");
    }

    /// <summary>
    /// Creates an assembly with the following type:
    /// public class SomeType
    /// {
    ///    public void SomeMethod {}
    /// }
    /// </summary>
    private Assembly CreateAssemblyWithSomeClassWithSomeMethod()
    {
      var assemblyName = new AssemblyName("SomeAssembly");
      var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
      var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
      var typeBuilder = moduleBuilder.DefineType("SomeType", TypeAttributes.Public);

      CreateSomeMethod(typeBuilder);

      typeBuilder.CreateType();

      return assemblyBuilder;
    }

    /// <summary>
    /// Creates an assembly with the following type:
    /// public class SomeType
    /// {
    ///    [ConsumerAttribute("topic-name")]
    ///    public void SomeMethod {}
    /// }
    /// </summary>
    private Assembly CreateAssemblyWithSomeClassWithAnnotatedSomeMethod()
    {
      var assemblyName = new AssemblyName("SomeAssembly");
      var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
      var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
      var typeBuilder = moduleBuilder.DefineType("SomeType", TypeAttributes.Public);

      var methodBuilder = CreateSomeMethod(typeBuilder);

      var attributeConstructor = typeof(ConsumerAttribute).GetConstructor(new Type[] { typeof(string) });
      var attributeBuilder = new CustomAttributeBuilder(attributeConstructor, new object[] { "topic-name" });
      methodBuilder.SetCustomAttribute(attributeBuilder);

      typeBuilder.CreateType();

      return assemblyBuilder;
    }

    private MethodBuilder CreateSomeMethod(TypeBuilder typeBuilder)
    {
      var methodBuilder = typeBuilder.DefineMethod("SomeMethod", MethodAttributes.Public);
      var methodIl = methodBuilder.GetILGenerator();
      methodIl.Emit(OpCodes.Nop);
      methodIl.Emit(OpCodes.Ret);

      return methodBuilder;
    }
  }
}
