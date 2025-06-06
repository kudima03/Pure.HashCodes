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
        byte[] typePrefix =
            [0, 69, 151, 1, 4, 52, 46, 126, 159, 32, 211, 174, 149, 230, 168, 150];

        byte[] valueBytes = "Hello, world!!!"u8.ToArray();
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

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
        byte[] typePrefix =
            [0, 69, 151, 1, 4, 52, 46, 126, 159, 32, 211, 174, 149, 230, 168, 150];

        byte[] valueBytes = "Hello, world!!!"u8.ToArray();
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

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
        byte[] typePrefix =
            [0, 69, 151, 1, 4, 52, 46, 126, 159, 32, 211, 174, 149, 230, 168, 150];

        byte[] valueBytes = "Hello, world!!!"u8.ToArray();
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

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