using System.Security.Cryptography;

namespace Pure.HashCodes.Tests;

public sealed record HashFromBytesTests
{
    [Fact]
    public void ProduceDeterminedCode()
    {
        Random random = new Random();
        byte[] bytes = Enumerable.Range(0, 1000).Select(_ => (byte)random.Next(0, 255)).ToArray();
        Assert.True(SHA256.HashData(bytes).SequenceEqual(new HashFromBytes(bytes)));
    }

    [Fact]
    public void ThrowsExceptionOnEmptyCollection()
    {
        Assert.Throws<ArgumentException>(() => new HashFromBytes([]).ToArray());
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromBytes([]).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromBytes([]).ToString());
    }
}