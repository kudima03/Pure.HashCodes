using System.Collections;
using Pure.HashCodes.Abstractions;

namespace Pure.HashCodes.Tests.Fakes;

public sealed record EnumerationCountingHash : IDeterminedHash
{
    private readonly IEnumerable<byte> _bytes;

    public EnumerationCountingHash(IEnumerable<byte> bytes)
    {
        _bytes = bytes;
    }

    public int Enumerations { get; private set; }

    public IEnumerator<byte> GetEnumerator()
    {
        Enumerations++;
        return _bytes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
