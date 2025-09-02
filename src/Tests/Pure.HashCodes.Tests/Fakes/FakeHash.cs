using System.Collections;

namespace Pure.HashCodes.Tests.Fakes;

public sealed record FakeHash : IDeterminedHash
{
    private readonly IEnumerable<byte> _bytes;

    public FakeHash(IEnumerable<byte> bytes)
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
}
