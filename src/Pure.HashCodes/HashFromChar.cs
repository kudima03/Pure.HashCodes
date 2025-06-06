using Pure.Primitives.Abstractions.Char;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromChar : IDeterminedHash
{
    private static readonly byte[] typePrefix =
        [254, 68, 151, 1, 12, 49, 216, 116, 190, 58, 148, 90, 142, 204, 225, 70];

    private readonly IChar _value;

    public HashFromChar(IChar value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(typePrefix.Concat(BitConverter.GetBytes(_value.CharValue))).GetEnumerator();
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