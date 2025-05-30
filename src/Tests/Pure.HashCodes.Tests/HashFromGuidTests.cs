using System.Collections;
using System.Security.Cryptography;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.HashCodes.Tests;

public sealed record HashFromGuidTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        const byte typeCode = 4;
        System.Guid guid = System.Guid.NewGuid();

        byte[] valueBytes = guid.ToByteArray();
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromGuid(new Guid(guid));

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
        const byte typeCode = 4;

        System.Guid guid = System.Guid.NewGuid();

        byte[] valueBytes = guid.ToByteArray();
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromGuid(new Guid(guid));

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
        const byte typeCode = 4;

        System.Guid guid = System.Guid.NewGuid();

        byte[] valueBytes = guid.ToByteArray();
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromGuid(new Guid(guid)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromGuid(new Guid()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromGuid(new Guid()).ToString());
    }
}