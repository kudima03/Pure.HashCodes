using Pure.Primitives.Abstractions.DayOfWeek;
using System.Collections;

namespace Pure.HashCodes.Internals;

internal sealed record HashFromDayOfWeek : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
        [104, 69, 151, 1, 244, 155, 254, 117, 160, 204, 149, 170, 27, 220, 11, 55];

    private readonly IDayOfWeek _value;

    public HashFromDayOfWeek(IDayOfWeek value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(TypePrefix.Concat(BitConverter.GetBytes(_value.DayNumberValue.NumberValue)))
            .GetEnumerator();
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