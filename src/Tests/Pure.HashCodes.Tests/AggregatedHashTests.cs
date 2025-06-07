using Pure.Primitives.Random.Bool;
using Pure.Primitives.Random.Char;
using Pure.Primitives.Random.DateTime;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.Time;
using System.Collections;

namespace Pure.HashCodes.Tests;

public sealed record AggregatedHashTests
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
            new DeterminedHash(new RandomDateTime())
        ];

        IEnumerable actualHash = new AggregatedHash(hashCollection);
        byte[] actualUntypedHash = new AggregatedHash(hashCollection).ToArray();

        int index = 0;
        bool equal = true;

        foreach (object element in actualHash)
        {
            if ((byte)element != actualUntypedHash[index++])
            {
                equal = false;
                break;
            }
        }

        Assert.True(equal);
    }

    [Fact]
    public void OrderNotMatters()
    {
        IReadOnlyCollection<IDeterminedHash> hashCollection =
        [
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar()),
            new DeterminedHash(new RandomUShort()),
            new DeterminedHash(new RandomFloat()),
            new DeterminedHash(new RandomTime()),
            new DeterminedHash(new RandomDateTime())
        ];

        Assert.True(new AggregatedHash(hashCollection).SequenceEqual(new AggregatedHash(hashCollection.Reverse())));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new AggregatedHash([]).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new AggregatedHash([]).ToString());
    }
}