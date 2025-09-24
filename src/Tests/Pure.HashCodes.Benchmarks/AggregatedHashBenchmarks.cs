using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Pure.HashCodes.Benchmarks.Fakes;

namespace Pure.HashCodes.Benchmarks;

[MemoryDiagnoser]
[JsonExporterAttribute.Full]
public class AggregatedHashBenchmarks
{
    private readonly IEnumerable<IDeterminedHash> _hashes =
    [
        .. Enumerable.Range(0, 1000).Select(_ => new RandomDeterminedHash()),
    ];

    [Benchmark]
    public int HashesMultiple()
    {
        return new AggregatedHash(_hashes).Count();
    }
}
