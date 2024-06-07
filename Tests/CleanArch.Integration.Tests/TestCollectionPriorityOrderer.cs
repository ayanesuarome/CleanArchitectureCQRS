using System.Reflection;
using Xunit.Abstractions;

namespace CleanArch.Integration.Tests;

/// <summary>
/// Custom xUnit test collection orderer that uses the order attribute.
/// </summary>
internal sealed class TestCollectionPriorityOrderer : ITestCollectionOrderer
{
    public const string TypeName = "CleanArch.Integration.Tests.TestCollectionPriorityOrderer";
    public const string AssemblyName = "CleanArch.Integration.Tests";

    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        return testCollections.OrderBy(GetOrder);
    }

    private static int GetOrder(ITestCollection testCollection)
    {
        var i = testCollection.DisplayName.LastIndexOf(' ');
        if (i <= -1)
        {
            return 0;
        }

        var className = testCollection.DisplayName.Substring(i + 1);
        var type = Type.GetType(className);
        
        if (type == null)
        {
            return 0;
        }

        var attr = type.GetCustomAttribute<TestPriorityAttribute>();

        return attr?.Priority ?? 0;
    }
}
