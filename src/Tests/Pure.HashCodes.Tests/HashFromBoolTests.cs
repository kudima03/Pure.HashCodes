using Pure.Primitives.Bool;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests;

public sealed record HashFromBoolTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        const byte typeCode = 0;

        byte[] valueBytes = BitConverter.GetBytes(true);
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

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
        const byte typeCode = 0;

        byte[] valueBytes = BitConverter.GetBytes(true);
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

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
        const byte typeCode = 0;

        byte[] valueBytes = BitConverter.GetBytes(true);
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromBool(new True()));
    }

    [Fact]
    public void ProduceDeterminedCodeOnFalse()
    {
        const byte typeCode = 0;

        byte[] valueBytes = BitConverter.GetBytes(false);
        byte[] valueBytesWithTypeCode = valueBytes.Prepend(typeCode).ToArray();

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