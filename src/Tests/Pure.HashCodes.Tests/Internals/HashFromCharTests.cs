using Pure.HashCodes.Internals;
using System.Collections;
using System.Security.Cryptography;
using Char = Pure.Primitives.Char.Char;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromCharTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
            [254, 68, 151, 1, 12, 49, 216, 116, 190, 58, 148, 90, 142, 204, 225, 70];

        byte[] valueBytes = BitConverter.GetBytes('H');
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromChar(new Char('H'));

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
            [254, 68, 151, 1, 12, 49, 216, 116, 190, 58, 148, 90, 142, 204, 225, 70];

        byte[] valueBytes = BitConverter.GetBytes('H');
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromChar(new Char('H'));

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
            [254, 68, 151, 1, 12, 49, 216, 116, 190, 58, 148, 90, 142, 204, 225, 70];

        byte[] valueBytes = BitConverter.GetBytes('H');
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromChar(new Char('H')));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromChar(new Char('H')).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromChar(new Char('H')).ToString());
    }
}