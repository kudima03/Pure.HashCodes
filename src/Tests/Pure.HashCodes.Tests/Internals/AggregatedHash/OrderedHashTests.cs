using Pure.Primitives.Random.Bool;
using Pure.Primitives.Random.Char;
using Pure.Primitives.Random.DateTime;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.Time;
using System.Collections;

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
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new OrderedHashes([]).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new OrderedHashes([]).ToString());
    }
}