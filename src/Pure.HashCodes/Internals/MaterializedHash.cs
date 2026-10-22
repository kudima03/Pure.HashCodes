using System.Collections;
using Pure.HashCodes.Abstractions;

namespace Pure.HashCodes.Internals;

internal sealed record MaterializedHash : IDeterminedHash
{
    private readonly IReadOnlyCollection<byte> _bytes;

    public MaterializedHash(IReadOnlyCollection<byte> bytes)
    {
        _bytes = bytes;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return _bytes.GetEnumerator();
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
