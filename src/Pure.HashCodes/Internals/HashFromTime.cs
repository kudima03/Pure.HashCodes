using System.Collections;
using Pure.HashCodes.Abstractions;
using Pure.Primitives.Abstractions.Time;

namespace Pure.HashCodes.Internals;

internal sealed record HashFromTime : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
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

    private readonly ITime _value;

    public HashFromTime(ITime value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        byte[] hourBytes = BitConverter.GetBytes(_value.Hour.NumberValue);
        byte[] minutesBytes = BitConverter.GetBytes(_value.Minute.NumberValue);
        byte[] secondBytes = BitConverter.GetBytes(_value.Second.NumberValue);
        byte[] millisecondsBytes = BitConverter.GetBytes(_value.Millisecond.NumberValue);
        byte[] microsecondsBytes = BitConverter.GetBytes(_value.Microsecond.NumberValue);
        byte[] nanosecondsBytes = BitConverter.GetBytes(_value.Nanosecond.NumberValue);

        IEnumerable<byte> concatenated = TypePrefix
            .Concat(hourBytes)
            .Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Concat(nanosecondsBytes);

        return new HashFromBytes(concatenated).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
