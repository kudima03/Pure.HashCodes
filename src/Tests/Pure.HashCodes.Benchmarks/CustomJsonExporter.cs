using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;

namespace Pure.HashCodes.Benchmarks;

internal sealed class CustomJsonExporter : IExporter
{
    public string Name => "Custom";

    public void ExportToLog(Summary summary, ILogger logger)
    {
        new JsonExporter().ExportToLog(summary, logger);
    }

    public IEnumerable<string> ExportToFiles(Summary summary, ILogger consoleLogger)
    {
        const string relativePath = "BenchmarkDotNet.Artifacts/results/output.json";

        using StreamLogger logger = new StreamLogger(new StreamWriter(relativePath));
        ExportToLog(summary, logger);
        return [relativePath];
    }
}
