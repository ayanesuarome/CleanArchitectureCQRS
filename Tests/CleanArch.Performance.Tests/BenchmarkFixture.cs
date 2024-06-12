using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using CleanArch.Benchmarking;

public class BenchmarkFixture
{
    public Summary BenchmarkSummary { get; }

    public BenchmarkFixture()
    {
        ManualConfig config = new()
        {
            SummaryStyle = SummaryStyle.Default.WithMaxParameterColumnWidth(100),
            Orderer = new DefaultOrderer(SummaryOrderPolicy.SlowestToFastest),
            Options = ConfigOptions.Default
        };

        BenchmarkSummary = BenchmarkRunner.Run<LeaveTypeBenchMarks>(config);
    }
}
