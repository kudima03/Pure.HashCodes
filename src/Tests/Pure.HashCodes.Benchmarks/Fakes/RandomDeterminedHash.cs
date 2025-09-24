using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pure.HashCodes.Benchmarks.Fakes;

internal sealed record RandomDeterminedHash : IDeterminedHash
{
    private readonly IEnumerable<byte> _hash = [.. Enumerable
        .Range(0, 32)
        .Select(_ => (byte)Random.Shared.Next(byte.MinValue, byte.MaxValue))];

    public IEnumerator<byte> GetEnumerator()
    {
        return _hash.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
