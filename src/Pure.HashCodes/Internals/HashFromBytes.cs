using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes;

internal sealed record HashFromBytes : IDeterminedHash
{
    private readonly IEnumerable<byte> _bytes;

    public HashFromBytes(IEnumerable<byte> bytes)
    {
        _bytes = bytes;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return SHA256.HashData(_bytes.ToArray()).AsEnumerable().GetEnumerator();
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
