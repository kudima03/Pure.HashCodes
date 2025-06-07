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
using Pure.Primitives.Number;
using Pure.Primitives.Random.Char;
using Pure.Primitives.Random.Date;
using Pure.Primitives.Random.DateTime;
using Pure.Primitives.Random.DayOfWeek;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.Primitives.Random.Time;
using System.Collections;

namespace Pure.HashCodes.Tests;

public sealed record DeterminedHashTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable determinedHash = new DeterminedHash(new True());
        IEnumerator boolHash = new HashFromBool(new True()).GetEnumerator();

        bool equal = true;

        boolHash.MoveNext();

        foreach (object item in determinedHash)
        {
            if (!item!.Equals(boolHash.Current))
            {
                equal = false;
                break;
            }

            boolHash.MoveNext();
        }

        Assert.True(equal);
    }

    [Fact]
    public void ProduceCorrectBoolHash()
    {
        IReadOnlyCollection<IBool> values = [new True(), new False()];

        Assert.Equal(
            values.Select(x => new HashFromBool(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectCharHash()
    {
        IEnumerable<IChar> values = new RandomCharCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromChar(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectDateHash()
    {
        IEnumerable<IDate> values = new RandomDateCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromDate(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectDateTimeHash()
    {
        IEnumerable<IDateTime> values = new RandomDateTimeCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromDateTime(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectDayOfWeekHash()
    {
        IEnumerable<IDayOfWeek> values = new RandomDayOfWeekCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromDayOfWeek(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectDoubleHash()
    {
        IEnumerable<INumber<double>> values = new RandomDoubleCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromDouble(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectFloatHash()
    {
        IEnumerable<INumber<float>> values = new RandomFloatCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromFloat(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectGuidHash()
    {
        IEnumerable<IGuid> values = Enumerable.Range(0, 1000).Select(_ => new Pure.Primitives.Guid.Guid()).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromGuid(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectIntHash()
    {
        IEnumerable<INumber<int>> values = new RandomIntCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromInt(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectStringHash()
    {
        IEnumerable<IString> values = new RandomStringCollection(new UShort(1000), new UShort(100)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromString(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectTimeHash()
    {
        IEnumerable<ITime> values = new RandomTimeCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromTime(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectUIntHash()
    {
        IEnumerable<INumber<uint>> values = new RandomUIntCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromUInt(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ProduceCorrectUShortHash()
    {
        IEnumerable<INumber<ushort>> values = new RandomUShortCollection(new UShort(1000)).ToArray();

        Assert.Equal(
            values.Select(x => new HashFromUShort(x)),
            values.Select(x => new DeterminedHash(x)),
            EqualityComparer<IDeterminedHash>.Create((hash1, hash2) => hash1!.SequenceEqual(hash2!)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new DeterminedHash(new True()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new DeterminedHash(new UShort(100)).ToString());
    }
}