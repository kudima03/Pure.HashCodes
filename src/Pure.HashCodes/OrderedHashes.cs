using System.Collections;

namespace Pure.HashCodes;

internal sealed record OrderedHashes : IEnumerable<IDeterminedHash>
{
    private readonly IEnumerable<IDeterminedHash> _hashes;

    public OrderedHashes(IEnumerable<IDeterminedHash> hashes)
    {
        _hashes = hashes;
    }

    public IEnumerator<IDeterminedHash> GetEnumerator()
    {
        return _hashes.Order(new DeterminedHashComparer()).GetEnumerator();
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