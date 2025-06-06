using Pure.Primitives.Abstractions.Guid;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromGuid : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
        [255, 68, 151, 1, 226, 166, 124, 113, 191, 194, 185, 246, 222, 172, 137, 178];

    private readonly IGuid _value;

    public HashFromGuid(IGuid value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(TypePrefix.Concat(_value.GuidValue.ToByteArray())).GetEnumerator();
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