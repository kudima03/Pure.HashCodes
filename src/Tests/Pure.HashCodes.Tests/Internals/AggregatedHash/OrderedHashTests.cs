using System.Collections;
using Pure.HashCodes.Abstractions;
using Pure.HashCodes.Internals.AggregatedHash;
using Pure.HashCodes.Tests.Fakes;
using Pure.Primitives.Random.Bool;
using Pure.Primitives.Random.Char;
using Pure.Primitives.Random.DateTime;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.Time;

namespace Pure.HashCodes.Tests.Internals.AggregatedHash;

public sealed record OrderedHashTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        IDeterminedHash[] hashCollection =
        [
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar()),
            new DeterminedHash(new RandomUShort()),
            new DeterminedHash(new RandomFloat()),
            new DeterminedHash(new RandomTime()),
            new DeterminedHash(new RandomDateTime()),
        ];

        IEnumerable actualHash = new OrderedHashes(hashCollection);
        IDeterminedHash[] actualTypedHash = new OrderedHashes(hashCollection).ToArray();

        int index = 0;
        bool equal = true;

        foreach (object element in actualHash)
        {
            if (!((IDeterminedHash)element).SequenceEqual(actualTypedHash[index++]))
            {
                equal = false;
                break;
            }
        }

        Assert.True(equal);
    }

    [Fact]
    public void ProduceOrder()
    {
        IReadOnlyCollection<IDeterminedHash> hashes =
        [
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar()),
            new DeterminedHash(new RandomUShort()),
            new DeterminedHash(new RandomFloat()),
            new DeterminedHash(new RandomTime()),
        ];

        Assert.True(
            hashes
                .Order(new DeterminedHashComparer())
                .Zip(new OrderedHashes(hashes), (x, y) => x.SequenceEqual(y))
                .Distinct()
                .Single()
        );
    }

    [Fact]
    public void EnumeratesEachHashOnce()
    {
        IReadOnlyCollection<EnumerationCountingHash> hashes =
        [
            .. Enumerable
                .Range(0, 64)
                .Select(i => new EnumerationCountingHash(BitConverter.GetBytes(i))),
        ];

        _ = new OrderedHashes(hashes).SelectMany(x => x).ToArray();

        Assert.All(hashes, hash => Assert.Equal(1, hash.Enumerations));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new OrderedHashes([]).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new OrderedHashes([]).ToString());
    }
}
