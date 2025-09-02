using System.Collections;
using System.Text;
using Pure.Primitives.Abstractions.String;

namespace Pure.HashCodes;

internal sealed record HashFromString : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        0,
        69,
        151,
        1,
        4,
        52,
        46,
        126,
        159,
        32,
        211,
        174,
        149,
        230,
        168,
        150,
    ];

    private readonly IString _value;

    public HashFromString(IString value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(
            TypePrefix.Concat(Encoding.UTF8.GetBytes(_value.TextValue))
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
