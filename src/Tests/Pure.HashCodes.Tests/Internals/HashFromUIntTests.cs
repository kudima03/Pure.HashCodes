using Pure.HashCodes.Internals;
using Pure.Primitives.Number;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromUIntTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
            [84, 69, 151, 1, 141, 237, 49, 120, 170, 80, 30, 69, 71, 102, 210, 119];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt32(123));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromUInt(new UInt(123));

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
            [84, 69, 151, 1, 141, 237, 49, 120, 170, 80, 30, 69, 71, 102, 210, 119];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt32(123));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromUInt(new UInt(123));

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
            [84, 69, 151, 1, 141, 237, 49, 120, 170, 80, 30, 69, 71, 102, 210, 119];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt32(123));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromUInt(new UInt(123)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromUInt(new UInt(123)).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromUInt(new UInt(123)).ToString());
    }
}