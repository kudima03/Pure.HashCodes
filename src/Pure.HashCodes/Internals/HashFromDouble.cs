using Pure.Primitives.Abstractions.Number;
using System.Collections;

namespace Pure.HashCodes;

internal sealed record HashFromDouble : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        95,
        69,
        151,
        1,
        107,
        226,
        94,
        115,
        132,
        197,
        237,
        217,
        148,
        67,
        244,
        5,
    ];

    private readonly INumber<double> _value;

    public HashFromDouble(INumber<double> value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(
            TypePrefix.Concat(BitConverter.GetBytes(_value.NumberValue))
        ).GetEnumerator();
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