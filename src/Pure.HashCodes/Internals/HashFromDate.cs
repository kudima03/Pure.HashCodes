using Pure.Primitives.Abstractions.Date;
using System.Collections;

namespace Pure.HashCodes;

internal sealed record HashFromDate : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
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

    private readonly IDate _value;

    public HashFromDate(IDate value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        byte[] yearBytes = BitConverter.GetBytes(_value.Year.NumberValue);
        byte[] monthBytes = BitConverter.GetBytes(_value.Month.NumberValue);
        byte[] dayBytes = BitConverter.GetBytes(_value.Day.NumberValue);

        IEnumerable<byte> concatenated = TypePrefix
            .Concat(yearBytes)
            .Concat(monthBytes)
            .Concat(dayBytes);

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