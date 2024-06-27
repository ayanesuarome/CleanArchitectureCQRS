using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using CleanArch.Benchmarking;

ManualConfig config = new()
{
    SummaryStyle = SummaryStyle.Default.WithMaxParameterColumnWidth(100),
    Orderer = new DefaultOrderer(SummaryOrderPolicy.SlowestToFastest),
    Options = ConfigOptions.Default
};

//BenchmarkRunner.Run<LeaveTypeBenchMarks>();
//BenchmarkRunner.Run<StringBenchMarks>();
BenchmarkRunner.Run<OutBoxMessageBenchMarks>();