using Pure.Primitives.Abstractions.Number;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromUShort : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
        [75, 69, 151, 1, 198, 16, 204, 119, 135, 66, 52, 31, 39, 64, 88, 38];

    private readonly INumber<ushort> _value;

    public HashFromUShort(INumber<ushort> value)
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