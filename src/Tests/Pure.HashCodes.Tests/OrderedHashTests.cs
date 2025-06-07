using Pure.Primitives.Random.Bool;
using Pure.Primitives.Random.Char;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.Time;

namespace Pure.HashCodes.Tests;

public sealed record OrderedHashTests
{
    [Fact]
    public void ProduceOrder()
    {
        IReadOnlyCollection<IDeterminedHash> hashes =
        [
            new DeterminedHash(new RandomBool()),
            new DeterminedHash(new RandomChar()),
            new DeterminedHash(new RandomUShort()),
            new DeterminedHash(new RandomFloat()),
            new DeterminedHash(new RandomTime())
        ];

        Assert.True(hashes.Order(new DeterminedHashComparer())
            .Zip(new OrderedHashes(hashes), (x, y) => x.SequenceEqual(y)).Distinct().Single());
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