using Pure.Primitives.Random.Bool;
using Pure.Primitives.Random.Char;
using Pure.Primitives.Random.DateTime;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.Time;
using System.Collections;

namespace Pure.HashCodes.Tests.Internals.AggregatedHash;

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

        IEnumerable actualHash = new HashCodes.AggregatedHash(hashCollection);
        byte[] actualUntypedHash = new HashCodes.AggregatedHash(hashCollection).ToArray();

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
    public void HashesEmptyCollection()
    {
        IReadOnlyCollection<IDeterminedHash> hashCollection = [];

        Assert.Equal("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855", 
            Convert.ToHexString(new HashCodes.AggregatedHash(hashCollection).ToArray()));
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

        Assert.True(new HashCodes.AggregatedHash(hashCollection).SequenceEqual(new HashCodes.AggregatedHash(hashCollection.Reverse())));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashCodes.AggregatedHash([]).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashCodes.AggregatedHash([]).ToString());
    }
}