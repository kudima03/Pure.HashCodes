using Pure.Primitives.Time;
using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes.Tests;

public sealed record HashFromTimeTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        const byte typeCode = 3;

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        byte[] hourBytes = BitConverter.GetBytes((ushort)time.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)time.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)time.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)time.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)time.Microsecond);

        byte[] concatenated = hourBytes.Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Prepend(typeCode)
            .ToArray();

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
        const byte typeCode = 3;

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        byte[] hourBytes = BitConverter.GetBytes((ushort)time.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)time.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)time.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)time.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)time.Microsecond);

        byte[] concatenated = hourBytes.Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Prepend(typeCode)
            .ToArray();

        byte[] expectedHash = SHA256.HashData(concatenated);

        IEnumerable<byte> actualHash = new HashFromTime(new Time(time));

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
        const byte typeCode = 3;

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        byte[] hourBytes = BitConverter.GetBytes((ushort)time.Hour);
        byte[] minutesBytes = BitConverter.GetBytes((ushort)time.Minute);
        byte[] secondBytes = BitConverter.GetBytes((ushort)time.Second);
        byte[] millisecondsBytes = BitConverter.GetBytes((ushort)time.Millisecond);
        byte[] microsecondsBytes = BitConverter.GetBytes((ushort)time.Microsecond);

        byte[] concatenated = 
            hourBytes.Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Prepend(typeCode)
            .ToArray();

        Assert.Equal(SHA256.HashData(concatenated), new HashFromTime(new Time(time)));
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromTime(new CurrentTime()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new HashFromTime(new CurrentTime()).ToString());
    }
}