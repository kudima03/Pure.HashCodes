using System.Collections;
using System.Security.Cryptography;
using Pure.HashCodes.Internals;
using Pure.Primitives.Date;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromDateTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
        [
            130,
            69,
            151,
            1,
            3,
            139,
            193,
            122,
            182,
            30,
            13,
            221,
            74,
            60,
            6,
            86,
        ];

        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        byte[] yearBytes = BitConverter.GetBytes((ushort)date.Year);
        byte[] monthBytes = BitConverter.GetBytes((ushort)date.Month);
        byte[] dayBytes = BitConverter.GetBytes((ushort)date.Day);

        byte[] concatenated = [.. typePrefix, .. yearBytes, .. monthBytes, .. dayBytes];

        byte[] expectedHash = SHA256.HashData(concatenated);

        IEnumerable actualHash = new HashFromDate(new Date(date));

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
            130,
            69,
            151,
            1,
            3,
            139,
            193,
            122,
            182,
            30,
            13,
            221,
            74,
            60,
            6,
            86,
        ];

        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        byte[] yearBytes = BitConverter.GetBytes((ushort)date.Year);
        byte[] monthBytes = BitConverter.GetBytes((ushort)date.Month);
        byte[] dayBytes = BitConverter.GetBytes((ushort)date.Day);

        byte[] concatenated = [.. typePrefix, .. yearBytes, .. monthBytes, .. dayBytes];

        byte[] expectedHash = SHA256.HashData(concatenated);

        IEnumerable<byte> actualHash = new HashFromDate(new Date(date));

        bool notEqual = false;

        foreach (
            (byte element, int index) in actualHash.Select(
                (element, index) => (element, index)
            )
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
            130,
            69,
            151,
            1,
            3,
            139,
            193,
            122,
            182,
            30,
            13,
            221,
            74,
            60,
            6,
            86,
        ];

        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        byte[] yearBytes = BitConverter.GetBytes((ushort)date.Year);
        byte[] monthBytes = BitConverter.GetBytes((ushort)date.Month);
        byte[] dayBytes = BitConverter.GetBytes((ushort)date.Day);

        byte[] concatenated = [.. typePrefix, .. yearBytes, .. monthBytes, .. dayBytes];

        Assert.Equal(SHA256.HashData(concatenated), new HashFromDate(new Date(date)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new HashFromDate(new CurrentDate()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new HashFromDate(new CurrentDate()).ToString()
        );
    }
}
