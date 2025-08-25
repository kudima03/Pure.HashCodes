using System.Collections;
using System.Security.Cryptography;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromGuidTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
        [
            255,
            68,
            151,
            1,
            226,
            166,
            124,
            113,
            191,
            194,
            185,
            246,
            222,
            172,
            137,
            178,
        ];

        System.Guid guid = System.Guid.NewGuid();

        byte[] valueBytes = guid.ToByteArray();
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

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
        byte[] typePrefix =
        [
            255,
            68,
            151,
            1,
            226,
            166,
            124,
            113,
            191,
            194,
            185,
            246,
            222,
            172,
            137,
            178,
        ];

        System.Guid guid = System.Guid.NewGuid();

        byte[] valueBytes = guid.ToByteArray();
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromGuid(new Guid(guid));

        bool notEqual = false;

        foreach (
            (byte element, int index) in actualHash.Select((element, index) => (element, index))
        )
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
        [
            255,
            68,
            151,
            1,
            226,
            166,
            124,
            113,
            191,
            194,
            185,
            246,
            222,
            172,
            137,
            178,
        ];

        System.Guid guid = System.Guid.NewGuid();

        byte[] valueBytes = guid.ToByteArray();
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

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