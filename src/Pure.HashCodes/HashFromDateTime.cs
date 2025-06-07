using Pure.Primitives.Abstractions.DateTime;
using System.Collections;

namespace Pure.HashCodes;

internal sealed record HashFromDateTime : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
        [139, 69, 151, 1, 214, 95, 189, 127, 179, 214, 20, 202, 15, 75, 55, 194];

    private readonly IDateTime _value;

    public HashFromDateTime(IDateTime value)
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
        byte[] yearBytes = BitConverter.GetBytes(_value.Year.NumberValue);
        byte[] monthBytes = BitConverter.GetBytes(_value.Month.NumberValue);
        byte[] dayBytes = BitConverter.GetBytes(_value.Day.NumberValue);

        IEnumerable<byte> concatenated = TypePrefix
            .Concat(yearBytes)
            .Concat(monthBytes)
            .Concat(dayBytes)
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