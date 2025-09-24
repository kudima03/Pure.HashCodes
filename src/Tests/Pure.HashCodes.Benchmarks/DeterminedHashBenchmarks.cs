using System.Linq;
using BenchmarkDotNet.Attributes;
using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.DateTime;
using Pure.Primitives.Abstractions.DayOfWeek;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Abstractions.Time;
using Pure.Primitives.Bool;
using Pure.Primitives.Char;
using Pure.Primitives.Date;
using Pure.Primitives.DateTime;
using Pure.Primitives.DayOfWeek;
using Pure.Primitives.Guid;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.Time;

namespace Pure.HashCodes.Benchmarks;

[MemoryDiagnoser]
public class DeterminedHashBenchmarks
{
    private readonly IBool _bool = new True();

    private readonly IChar _char = new Char('A');

    private readonly ITime _time = new CurrentTime();

    private readonly IDate _date = new CurrentDate();

    private readonly IDateTime _dateTime = new CurrentDateTime();

    private readonly IDayOfWeek _dayOfWeek = new Friday();

    private readonly INumber<double> _double = new MaxDouble();

    private readonly INumber<float> _float = new MaxFloat();

    private readonly INumber<int> _int = new MaxInt();

    private readonly INumber<uint> _uint = new MaxUint();

    private readonly INumber<ushort> _ushort = new MaxUshort();

    private readonly IGuid _guid = new Guid();

    private readonly IString _string = new String(
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit"
    );

    [Benchmark]
    public int HashesBool()
    {
        return new DeterminedHash(_bool).Count();
    }

    [Benchmark]
    public int HashesChar()
    {
        return new DeterminedHash(_char).Count();
    }

    [Benchmark]
    public int HashesTime()
    {
        return new DeterminedHash(_time).Count();
    }

    [Benchmark]
    public int HashesDate()
    {
        return new DeterminedHash(_date).Count();
    }

    [Benchmark]
    public int HashesDateTime()
    {
        return new DeterminedHash(_dateTime).Count();
    }

    [Benchmark]
    public int HashesDayOfWeek()
    {
        return new DeterminedHash(_dayOfWeek).Count();
    }

    [Benchmark]
    public int HashesDouble()
    {
        return new DeterminedHash(_double).Count();
    }

    [Benchmark]
    public int HashesFloat()
    {
        return new DeterminedHash(_float).Count();
    }

    [Benchmark]
    public int HashesInt()
    {
        return new DeterminedHash(_int).Count();
    }

    [Benchmark]
    public int HashesUint()
    {
        return new DeterminedHash(_uint).Count();
    }

    [Benchmark]
    public int HashesUshort()
    {
        return new DeterminedHash(_ushort).Count();
    }

    [Benchmark]
    public int HashesGuid()
    {
        return new DeterminedHash(_guid).Count();
    }

    [Benchmark]
    public int HashesString()
    {
        return new DeterminedHash(_string).Count();
    }

    [Benchmark]
    public int HashesBytes()
    {
        return new DeterminedHash(Enumerable.Repeat(byte.MaxValue, 100000)).Count();
    }
}
