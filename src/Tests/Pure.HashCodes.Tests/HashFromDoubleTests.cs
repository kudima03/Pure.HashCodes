using Pure.Primitives.Number;
using System.Collections;
using System.Security.Cryptography;
using Double = Pure.Primitives.Number.Double;

namespace Pure.HashCodes.Tests;

public sealed record HashFromDoubleTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
            [95, 69, 151, 1, 107, 226, 94, 115, 132, 197, 237, 217, 148, 67, 244, 5];

        byte[] valueBytes = BitConverter.GetBytes(123.456D);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromDouble(new Double(123.456D));

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
            [95, 69, 151, 1, 107, 226, 94, 115, 132, 197, 237, 217, 148, 67, 244, 5];

        byte[] valueBytes = BitConverter.GetBytes(123.456D);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromDouble(new Double(123.456D));

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
            [95, 69, 151, 1, 107, 226, 94, 115, 132, 197, 237, 217, 148, 67, 244, 5];

        byte[] valueBytes = BitConverter.GetBytes(123.456D);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromDouble(new Double(123.456D)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromDouble(new Double(123.456D)).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromDouble(new Double(123.456D)).ToString());
    }
}