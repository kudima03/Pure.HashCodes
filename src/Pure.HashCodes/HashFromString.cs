using Pure.Primitives.Abstractions.String;
using System.Collections;
using System.Text;

namespace Pure.HashCodes;

public sealed record HashFromString : IDeterminedHash
{
    private const byte typePrefix = 2;

    private readonly IString _value;

    public HashFromString(IString value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(Encoding.UTF8.GetBytes(_value.TextValue).Prepend(typePrefix)).GetEnumerator();
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