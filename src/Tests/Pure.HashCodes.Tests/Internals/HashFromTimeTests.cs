using System.Collections;
using System.Security.Cryptography;
using Pure.HashCodes.Internals;
using Pure.Primitives.Time;

namespace Pure.HashCodes.Tests.Internals;

public sealed record HashFromTimeTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        byte[] typePrefix =
        [
            2,
            69,
            151,
            1,
            242,
            64,
            126,
            119,
            167,
            82,
            211,
            125,
            202,
            137,
            42,
            33,
        ];

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        byte[] hourBytes = BitConverter.GetBytes((ushort)time.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)time.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)time.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)time.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)time.Microsecond);
        byte[] nanosecondBytes = BitConverter.GetBytes((ushort)time.Nanosecond);

        byte[] concatenated =
        [
            .. typePrefix,
            .. hourBytes,
            .. minutesBytes,
            .. secondBytes,
            .. millisecondsBytes,
            .. microsecondsBytes,
            .. nanosecondBytes,
        ];

        byte[] expectedHash = SHA256.HashData(concatenated);

        IEnumerable actualHash = new HashFromTime(new Time(time));

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
            2,
            69,
            151,
            1,
            242,
            64,
            126,
            119,
            167,
            82,
            211,
            125,
            202,
            137,
            42,
            33,
        ];

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        byte[] hourBytes = BitConverter.GetBytes((ushort)time.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)time.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)time.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)time.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)time.Microsecond);
        byte[] nanosecondBytes = BitConverter.GetBytes((ushort)time.Nanosecond);

        byte[] concatenated =
        [
            .. typePrefix,
            .. hourBytes,
            .. minutesBytes,
            .. secondBytes,
            .. millisecondsBytes,
            .. microsecondsBytes,
            .. nanosecondBytes,
        ];

        byte[] expectedHash = SHA256.HashData(concatenated);

        IEnumerable<byte> actualHash = new HashFromTime(new Time(time));

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
            2,
            69,
            151,
            1,
            242,
            64,
            126,
            119,
            167,
            82,
            211,
            125,
            202,
            137,
            42,
            33,
        ];

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        byte[] hourBytes = BitConverter.GetBytes((ushort)time.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)time.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)time.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)time.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)time.Microsecond);
        byte[] nanosecondBytes = BitConverter.GetBytes((ushort)time.Nanosecond);

        byte[] concatenated =
        [
            .. typePrefix,
            .. hourBytes,
            .. minutesBytes,
            .. secondBytes,
            .. millisecondsBytes,
            .. microsecondsBytes,
            .. nanosecondBytes,
        ];

        Assert.Equal(SHA256.HashData(concatenated), new HashFromTime(new Time(time)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new HashFromTime(new CurrentTime()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new HashFromTime(new CurrentTime()).ToString()
        );
    }
}
