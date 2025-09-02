using System.Collections;
using Pure.HashCodes.Internals;
using Pure.HashCodes.Internals.AggregatedHash;

namespace Pure.HashCodes;

public sealed record AggregatedHash : IDeterminedHash
{
    private readonly IEnumerable<IDeterminedHash> _hashes;

    public AggregatedHash(params IDeterminedHash[] hashes)
        : this(hashes.AsReadOnly()) { }

    public AggregatedHash(IEnumerable<IDeterminedHash> hashes)
    {
        _hashes = hashes;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new HashFromBytes(
            new OrderedHashes(_hashes).SelectMany(x => x)
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
