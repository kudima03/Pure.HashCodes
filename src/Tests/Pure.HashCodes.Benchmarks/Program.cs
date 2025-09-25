using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Pure.HashCodes.Benchmarks;

_ = BenchmarkRunner.Run(
    typeof(Program).Assembly,
    DefaultConfig
        .Instance.WithOptions(ConfigOptions.JoinSummary)
        .AddExporter(new CustomJsonExporter()),
    args
);
