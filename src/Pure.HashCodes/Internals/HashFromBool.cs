using Pure.Primitives.Abstractions.Bool;
using System.Collections;

namespace Pure.HashCodes.Internals;

internal sealed record HashFromBool : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
        [249, 68, 151, 1, 220, 206, 245, 124, 153, 201, 213, 10, 215, 253, 42, 156];

    private readonly IBool _value;

    public HashFromBool(IBool value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(TypePrefix.Concat(BitConverter.GetBytes(_value.BoolValue))).GetEnumerator();
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