using BenchmarkDotNet.Running;
using Pure.HashCodes.Benchmarks;

_ = BenchmarkRunner.Run<AggregatedHashBenchmarks>();
