using System.Collections;
using System.Security.Cryptography;
using Pure.Primitives.Date;
using Pure.Primitives.DateTime;
using Pure.Primitives.Time;
using DateTime = Pure.Primitives.DateTime.DateTime;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromDateTimeTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
        [
            139,
            69,
            151,
            1,
            214,
            95,
            189,
            127,
            179,
            214,
            20,
            202,
            15,
            75,
            55,
            194,
        ];

        System.DateTime dateTime = System.DateTime.Now;

        byte[] yearBytes = BitConverter.GetBytes((ushort)dateTime.Year);
        byte[] monthBytes = BitConverter.GetBytes((ushort)dateTime.Month);
        byte[] dayBytes = BitConverter.GetBytes((ushort)dateTime.Day);
        byte[] hourBytes = BitConverter.GetBytes((ushort)dateTime.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)dateTime.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)dateTime.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)dateTime.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)dateTime.Microsecond);
        byte[] nanosecondBytes = BitConverter.GetBytes((ushort)dateTime.Nanosecond);

        byte[] concatenated = typePrefix
            .Concat(yearBytes)
            .Concat(monthBytes)
            .Concat(dayBytes)
            .Concat(hourBytes)
            .Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Concat(nanosecondBytes)
            .ToArray();

        byte[] expectedHash = SHA256.HashData(concatenated);

        IEnumerable actualHash = new HashFromDateTime(
            new DateTime(
                new Date(DateOnly.FromDateTime(dateTime)),
                new Time(TimeOnly.FromDateTime(dateTime))
            )
        );

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
            139,
            69,
            151,
            1,
            214,
            95,
            189,
            127,
            179,
            214,
            20,
            202,
            15,
            75,
            55,
            194,
        ];

        System.DateTime dateTime = System.DateTime.Now;

        byte[] yearBytes = BitConverter.GetBytes((ushort)dateTime.Year);
        byte[] monthBytes = BitConverter.GetBytes((ushort)dateTime.Month);
        byte[] dayBytes = BitConverter.GetBytes((ushort)dateTime.Day);
        byte[] hourBytes = BitConverter.GetBytes((ushort)dateTime.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)dateTime.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)dateTime.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)dateTime.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)dateTime.Microsecond);
        byte[] nanosecondBytes = BitConverter.GetBytes((ushort)dateTime.Nanosecond);

        byte[] concatenated = typePrefix
            .Concat(yearBytes)
            .Concat(monthBytes)
            .Concat(dayBytes)
            .Concat(hourBytes)
            .Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Concat(nanosecondBytes)
            .ToArray();

        byte[] expectedHash = SHA256.HashData(concatenated);

        IEnumerable<byte> actualHash = new HashFromDateTime(
            new DateTime(
                new Date(DateOnly.FromDateTime(dateTime)),
                new Time(TimeOnly.FromDateTime(dateTime))
            )
        );

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
            139,
            69,
            151,
            1,
            214,
            95,
            189,
            127,
            179,
            214,
            20,
            202,
            15,
            75,
            55,
            194,
        ];

        System.DateTime dateTime = System.DateTime.Now;

        byte[] yearBytes = BitConverter.GetBytes((ushort)dateTime.Year);
        byte[] monthBytes = BitConverter.GetBytes((ushort)dateTime.Month);
        byte[] dayBytes = BitConverter.GetBytes((ushort)dateTime.Day);
        byte[] hourBytes = BitConverter.GetBytes((ushort)dateTime.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)dateTime.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)dateTime.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)dateTime.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)dateTime.Microsecond);
        byte[] nanosecondBytes = BitConverter.GetBytes((ushort)dateTime.Nanosecond);

        byte[] concatenated = typePrefix
            .Concat(yearBytes)
            .Concat(monthBytes)
            .Concat(dayBytes)
            .Concat(hourBytes)
            .Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Concat(nanosecondBytes)
            .ToArray();

        Assert.Equal(
            SHA256.HashData(concatenated),
            new HashFromDateTime(
                new DateTime(
                    new Date(DateOnly.FromDateTime(dateTime)),
                    new Time(TimeOnly.FromDateTime(dateTime))
                )
            )
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() =>
            new HashFromDateTime(new CurrentDateTime()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() =>
            new HashFromDateTime(new CurrentDateTime()).ToString()
        );
    }
}
