using System.Collections;
using Pure.HashCodes.Abstractions;
using Pure.Primitives.Abstractions.Number;

namespace Pure.HashCodes.Internals;

internal sealed record HashFromInt : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        99,
        69,
        151,
        1,
        197,
        101,
        86,
        125,
        182,
        6,
        168,
        251,
        101,
        41,
        230,
        254,
    ];

    private readonly INumber<int> _value;

    public HashFromInt(INumber<int> value)
    {
        _value = value;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(
            TypePrefix.Concat(BitConverter.GetBytes(_value.NumberValue))
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
