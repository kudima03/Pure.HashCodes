using Pure.Primitives.Abstractions.Time;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromTime : IDeterminedHash
{
    private const byte typePrefix = 3;

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

        IEnumerable<byte> concatenated = hourBytes.Concat(minutesBytes)
            .Concat(secondBytes)
            .Concat(millisecondsBytes)
            .Concat(microsecondsBytes)
            .Prepend(typePrefix);

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