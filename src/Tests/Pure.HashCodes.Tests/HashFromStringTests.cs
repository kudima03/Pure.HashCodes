using Pure.Primitives.String;
using System.Collections;
using System.Security.Cryptography;
using String = Pure.Primitives.String.String;

namespace Pure.HashCodes.Tests;

public sealed record HashFromStringTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        const byte typeCode = 2;

        byte[] valueBytes = "Hello, world!!!"u8.ToArray();
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromString(new String("Hello, world!!!"));

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
        const byte typeCode = 2;

        byte[] valueBytes = "Hello, world!!!"u8.ToArray();
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromString(new String("Hello, world!!!"));

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
    public void ProduceDeterminedHash()
    {
        const byte typeCode = 2;

        byte[] valueBytes = "Hello, world!!!"u8.ToArray();
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromString(new String("Hello, world!!!")));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromString(new EmptyString()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromString(new EmptyString()).ToString());
    }
}