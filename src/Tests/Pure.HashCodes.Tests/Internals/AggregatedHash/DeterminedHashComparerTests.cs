using Pure.HashCodes.Tests.Fakes;

namespace Pure.HashCodes.Tests.Internals.AggregatedHash;

public sealed record DeterminedHashComparerTests
{
    [Fact]
    public void CompareLexicographically()
    {
        IDeterminedHash hash1 = new FakeHash([6, 5, 4, 3, 2, 1]);
        IDeterminedHash hash2 = new FakeHash([1, 2, 3, 4, 5, 6]);

        Assert.True(new DeterminedHashComparer().Compare(hash1, hash2) > 0);
    }

    [Fact]
    public void CompareCorrectlySameCollections()
    {
        IDeterminedHash hash1 = new FakeHash([1, 2, 3, 4, 5, 6]);
        IDeterminedHash hash2 = new FakeHash([1, 2, 3, 4, 5, 6]);

        Assert.Equal(0, new DeterminedHashComparer().Compare(hash1, hash2));
    }

    [Fact]
    public void CompareCorrectlyDifferentLength()
    {
        IDeterminedHash hash1 = new FakeHash([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
        IDeterminedHash hash2 = new FakeHash([1, 2, 3, 4, 5, 6]);

        Assert.True(new DeterminedHashComparer().Compare(hash1, hash2) > 0);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() =>
            new DeterminedHashComparer().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() =>
            new DeterminedHashComparer().ToString()
        );
    }
}
