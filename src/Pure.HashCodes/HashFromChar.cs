using Pure.Primitives.Abstractions.Char;
using System.Collections;

namespace Pure.HashCodes;

public sealed record HashFromChar : IDeterminedHash
{
    private const byte typePrefix = 1;

    private readonly IChar _value;

    public HashFromChar(IChar value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(BitConverter.GetBytes(_value.CharValue).Prepend(typePrefix)).GetEnumerator();
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