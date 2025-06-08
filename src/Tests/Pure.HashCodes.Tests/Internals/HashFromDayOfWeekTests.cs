using Pure.Primitives.DayOfWeek;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromDayOfWeekTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
            [104, 69, 151, 1, 244, 155, 254, 117, 160, 204, 149, 170, 27, 220, 11, 55];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt16(2));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable actualHash = new HashFromDayOfWeek(new Tuesday());

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
            [104, 69, 151, 1, 244, 155, 254, 117, 160, 204, 149, 170, 27, 220, 11, 55];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt16(3));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        byte[] expectedHash = SHA256.HashData(valueBytesWithTypeCode);

        IEnumerable<byte> actualHash = new HashFromDayOfWeek(new Wednesday());

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
            [104, 69, 151, 1, 244, 155, 254, 117, 160, 204, 149, 170, 27, 220, 11, 55];

        byte[] valueBytes = BitConverter.GetBytes(Convert.ToUInt16(4));
        byte[] valueBytesWithTypeCode = typePrefix.Concat(valueBytes).ToArray();

        Assert.Equal(SHA256.HashData(valueBytesWithTypeCode), new HashFromDayOfWeek(new Thursday()));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromDayOfWeek(new Tuesday()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromDayOfWeek(new Tuesday()).ToString());
    }
}