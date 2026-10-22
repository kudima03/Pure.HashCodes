using System.Collections;
using Pure.HashCodes.Internals;

namespace Pure.HashCodes.Tests.Internals;

public sealed record MaterializedHashTests
{
    [Fact]
    public void EnumeratesAsTyped()
    {
        byte[] bytes = [1, 2, 3, 4, 5, 6];

        Assert.Equal(bytes, new MaterializedHash(bytes));
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] bytes = [1, 2, 3, 4, 5, 6];

        IEnumerable actualHash = new MaterializedHash(bytes);
        List<byte> actualBytes = [];

        foreach (object element in actualHash)
        {
            actualBytes.Add((byte)element);
        }

        Assert.Equal(bytes, actualBytes);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new MaterializedHash([]).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new MaterializedHash([]).ToString());
    }
}
