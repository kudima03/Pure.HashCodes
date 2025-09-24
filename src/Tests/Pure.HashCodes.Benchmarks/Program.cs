using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

_ = BenchmarkRunner.Run(
    typeof(Program).Assembly,
    DefaultConfig.Instance.WithOptions(ConfigOptions.JoinSummary),
    args
);
