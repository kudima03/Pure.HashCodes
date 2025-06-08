using Pure.Primitives.Bool;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromBoolTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
            [249, 68, 151, 1, 220, 206, 245, 124, 153, 201, 213, 10, 215, 253, 42, 156];

        byte[] valueBytes = BitConverter.GetBytes(true);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromBool(new True());

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
            [249, 68, 151, 1, 220, 206, 245, 124, 153, 201, 213, 10, 215, 253, 42, 156];

        byte[] valueBytes = BitConverter.GetBytes(true);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromBool(new True());

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
    public void ProduceDeterminedCodeOnTrue()
    {
        byte[] typePrefix =
            [249, 68, 151, 1, 220, 206, 245, 124, 153, 201, 213, 10, 215, 253, 42, 156];

        byte[] valueBytes = BitConverter.GetBytes(true);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromBool(new True()));
    }

    [Fact]
    public void ProduceDeterminedCodeOnFalse()
    {
        byte[] typePrefix =
            [249, 68, 151, 1, 220, 206, 245, 124, 153, 201, 213, 10, 215, 253, 42, 156];

        byte[] valueBytes = BitConverter.GetBytes(false);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromBool(new False()));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromBool(new True()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromBool(new True()).ToString());
    }
}