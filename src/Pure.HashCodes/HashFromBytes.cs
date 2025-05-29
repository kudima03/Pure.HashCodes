using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes;

public sealed record HashFromBytes : IDeterminedHash
{
    private readonly IEnumerable<byte> _bytes;

    public HashFromBytes(IEnumerable<byte> bytes)
    {
        _bytes = bytes;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        if (!_bytes.Any())
        {
            throw new ArgumentException();
        }

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