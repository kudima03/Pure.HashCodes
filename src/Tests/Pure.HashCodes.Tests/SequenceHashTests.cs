using System.Collections;
using System.Security.Cryptography;
using Pure.HashCodes.Abstractions;
using Pure.HashCodes.Tests.Fakes;
using Pure.Primitives.Bool;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Bool;
using Pure.Primitives.Random.Char;
using Pure.Primitives.Random.DateTime;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.Time;

namespace Pure.HashCodes.Tests;

public sealed record SequenceHashTests
{
    private static readonly byte[] TypePrefix =
    [
        130,
        99,
        161,
        1,
        231,
        60,
        66,
        113,
        190,
        134,
        100,
        126,
        26,
        6,
        127,
        187,
    ];

    [Fact]
    public void ProduceCorrectHash()
    {
        IDeterminedHash first = new DeterminedHash(new True());
        IDeterminedHash second = new DeterminedHash(new UShort(100));

        Assert.Equal(
            SHA256.HashData([.. TypePrefix, .. first, .. second]),
            new SequenceHash(first, second)
        );
    }

    [Fact]
    public void HashesEmptyCollection()
    {
        Assert.Equal(SHA256.HashData(TypePrefix), new SequenceHash([]));
    }

    [Fact]
    public void ProduceSameHashForSameSequence()
    {
        IReadOnlyCollection<IDeterminedHash> hashCollection =
        [
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar()),
            new DeterminedHash(new RandomUShort()),
        ];

        Assert.Equal(new SequenceHash(hashCollection), new SequenceHash(hashCollection));
    }

    [Fact]
    public void OrderMatters()
    {
        IReadOnlyCollection<IDeterminedHash> hashCollection =
        [
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar()),
            new DeterminedHash(new RandomUShort()),
            new DeterminedHash(new RandomFloat()),
            new DeterminedHash(new RandomTime()),
            new DeterminedHash(new RandomDateTime()),
        ];

        Assert.NotEqual(
            new SequenceHash(hashCollection),
            new SequenceHash(hashCollection.Reverse())
        );
    }

    [Fact]
    public void DiffersFromAggregatedHash()
    {
        IReadOnlyCollection<IDeterminedHash> hashCollection =
        [
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar()),
        ];

        Assert.NotEqual(
            new DeterminedHash(hashCollection),
            new SequenceHash(hashCollection)
        );
    }

    [Fact]
    public void EnumeratesEachHashOnce()
    {
        IReadOnlyCollection<EnumerationCountingHash> hashes =
        [
            .. Enumerable
                .Range(0, 8)
                .Select(i => new EnumerationCountingHash(BitConverter.GetBytes(i))),
        ];

        _ = new SequenceHash(hashes).ToArray();

        Assert.All(hashes, hash => Assert.Equal(1, hash.Enumerations));
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IDeterminedHash hash = new SequenceHash(
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar())
        );

        Assert.Equal(hash, ((IEnumerable)hash).Cast<byte>());
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() => new SequenceHash([]).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new SequenceHash([]).ToString());
    }
}
