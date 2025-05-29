using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests;

public sealed record HashFromBytesTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        Random random = new Random();
        byte[] bytes = Enumerable.Range(0, 1000).Select(_ => (byte)random.Next(0, 255)).ToArray();

        byte[] expectedHash = SHA256.HashData(bytes);

        IEnumerable actualHash = new HashFromBytes(bytes);

        bool notEqual = false;
        int index = 0;

        foreach (object element in actualHash)
        {
            if ((byte)element != expectedHash[index++])
            {
                notEqual = true;
                break;
            }
        }

        Assert.False(notEqual);
    }

    [Fact]
    public void EnumeratesAsTyped()
    {
        Random random = new Random();
        byte[] bytes = Enumerable.Range(0, 1000).Select(_ => (byte)random.Next(0, 255)).ToArray();

        byte[] expectedHash = SHA256.HashData(bytes);

        IEnumerable<byte> actualHash = new HashFromBytes(bytes);

        bool notEqual = false;

        foreach ((byte element, int index) in actualHash.Select((element, index) => (element, index)))
        {
            if (element != expectedHash[index])
            {
                notEqual = true;
                break;
            }
        }

        Assert.False(notEqual);
    }

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