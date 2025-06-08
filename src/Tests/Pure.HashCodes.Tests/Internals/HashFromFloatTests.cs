using Pure.Primitives.Number;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromFloatTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
            [89, 69, 151, 1, 160, 235, 84, 118, 142, 173, 38, 139, 157, 90, 104, 161];

        byte[] valueBytes = BitConverter.GetBytes(123.456F);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromFloat(new Float(123.456F));

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
            [89, 69, 151, 1, 160, 235, 84, 118, 142, 173, 38, 139, 157, 90, 104, 161];

        byte[] valueBytes = BitConverter.GetBytes(123.456F);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromFloat(new Float(123.456F));

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
            [89, 69, 151, 1, 160, 235, 84, 118, 142, 173, 38, 139, 157, 90, 104, 161];

        byte[] valueBytes = BitConverter.GetBytes(123.456F);
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromFloat(new Float(123.456F)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromFloat(new Float(123.456F)).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromFloat(new Float(123.456F)).ToString());
    }
}