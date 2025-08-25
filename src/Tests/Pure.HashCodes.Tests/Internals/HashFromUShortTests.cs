using Pure.Primitives.Number;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromUShortTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix = [75, 69, 151, 1, 198, 16, 204, 119, 135, 66, 52, 31, 39, 64, 88, 38];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt16(123));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromUShort(new UShort(123));

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
        byte[] typePrefix = [75, 69, 151, 1, 198, 16, 204, 119, 135, 66, 52, 31, 39, 64, 88, 38];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt16(123));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromUShort(new UShort(123));

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
        byte[] typePrefix = [75, 69, 151, 1, 198, 16, 204, 119, 135, 66, 52, 31, 39, 64, 88, 38];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt16(123));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromUShort(new UShort(123)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() =>
            new HashFromUShort(new UShort(123)).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromUShort(new UShort(123)).ToString());
    }
}