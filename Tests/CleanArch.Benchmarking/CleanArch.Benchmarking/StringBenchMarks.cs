using BenchmarkDotNet.Attributes;
using System.Text;

namespace CleanArch.Benchmarking;

public class StringBenchMarks
{
    [Benchmark]
    public string StringConcatenation()
    {
        string type = "Type";
        return $"{nameof(StringBenchMarks)}.{type}";
    }

    [Benchmark]
    public string StringBuilder()
    {
        string type = "Type";

        StringBuilder sb = new(nameof(StringBenchMarks));
        sb.Append(type);
        
        return sb.ToString();
    }
}
