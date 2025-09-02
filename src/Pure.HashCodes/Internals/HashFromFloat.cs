using System.Collections;
using Pure.Primitives.Abstractions.Number;

namespace Pure.HashCodes;

internal sealed record HashFromFloat : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        89,
        69,
        151,
        1,
        160,
        235,
        84,
        118,
        142,
        173,
        38,
        139,
        157,
        90,
        104,
        161,
    ];

    private readonly INumber<float> _value;

    public HashFromFloat(INumber<float> value)
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
