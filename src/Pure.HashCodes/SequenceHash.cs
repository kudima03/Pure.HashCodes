using System.Collections;
using Pure.HashCodes.Abstractions;
using Pure.HashCodes.Internals;

namespace Pure.HashCodes;

public sealed record SequenceHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        130,
        99,
        161,
        1,
        231,
        60,
        66,
        113,
        190,
        134,
        100,
        126,
        26,
        6,
        127,
        187,
    ];

    private readonly IEnumerable<IDeterminedHash> _hashes;

    public SequenceHash(params IDeterminedHash[] hashes)
        : this(hashes.AsReadOnly()) { }

    public SequenceHash(IEnumerable<IDeterminedHash> hashes)
    {
        _hashes = hashes;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(
            TypePrefix.Concat(_hashes.SelectMany(x => x))
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
