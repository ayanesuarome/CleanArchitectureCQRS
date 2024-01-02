using System.Collections;

namespace CleanArch.Application.Tests.Features.Mocks;

public class InvalidStringClassData : IEnumerable<object[]>
{
    private readonly IEnumerable<object[]> _data = new List<object[]>
    {
        new object[] { null },
        new object[] { string.Empty },
        new object[] { " " }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
