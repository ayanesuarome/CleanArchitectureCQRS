using BenchmarkDotNet.Reports;
using System.Collections.Immutable;

namespace CleanArch.Performance.Tests;

public class LeaveTypePerformanceTest : IClassFixture<BenchmarkFixture>
{
    private readonly ImmutableArray<BenchmarkReport> _benchmarkReports;
    private readonly BenchmarkReport _anyLeaveTypeReport;

    public LeaveTypePerformanceTest(BenchmarkFixture benchmarkFixture)
    {
        _benchmarkReports = benchmarkFixture.BenchmarkSummary.Reports;
        _anyLeaveTypeReport = _benchmarkReports.First(x =>
           x.BenchmarkCase.Descriptor.DisplayInfo == "LeaveTypeBenchMarks.AnyLeaveTypeAsync");
    }

    //[Fact]
    public void Test()
    {
        var stats = _anyLeaveTypeReport.ResultStatistics;
        Assert.True(stats is { Mean: < 15 }, $"Mean was {stats.Mean}");
    }
}
