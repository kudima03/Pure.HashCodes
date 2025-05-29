using Pure.Primitives.Abstractions.Bool;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromBool : IDeterminedHash
{
    private const byte typePrefix = 0;

    private readonly IBool _value;

    public HashFromBool(IBool value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(BitConverter.GetBytes(_value.BoolValue).Prepend(typePrefix)).GetEnumerator();
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