using Pure.Primitives.Abstractions.Guid;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromGuid : IDeterminedHash
{
    private const byte typePrefix = 4;

    private readonly IGuid _value;

    public HashFromGuid(IGuid value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(_value.GuidValue.ToByteArray().Prepend(typePrefix)).GetEnumerator();
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