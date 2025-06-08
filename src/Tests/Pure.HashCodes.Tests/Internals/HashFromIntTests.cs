using Pure.Primitives.Number;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromIntTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
            [99, 69, 151, 1, 197, 101, 86, 125, 182, 6, 168, 251, 101, 41, 230, 254];

        byte[] valueBytes = BitConverter.GetBytes(123);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromInt(new Int(123));

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
            [99, 69, 151, 1, 197, 101, 86, 125, 182, 6, 168, 251, 101, 41, 230, 254];

        byte[] valueBytes = BitConverter.GetBytes(123);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromInt(new Int(123));

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
            [99, 69, 151, 1, 197, 101, 86, 125, 182, 6, 168, 251, 101, 41, 230, 254];

        byte[] valueBytes = BitConverter.GetBytes(123);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromInt(new Int(123)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromInt(new Int(123)).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromInt(new Int(123)).ToString());
    }
}