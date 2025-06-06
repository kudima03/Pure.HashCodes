using Pure.Primitives.Abstractions.Number;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromUInt : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
        [84, 69, 151, 1, 141, 237, 49, 120, 170, 80, 30, 69, 71, 102, 210, 119];

    private readonly INumber<uint> _value;

    public HashFromUInt(INumber<uint> value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(TypePrefix.Concat(BitConverter.GetBytes(_value.NumberValue))).GetEnumerator();
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