using System.Collections;
using System.Collections.Generic;

namespace Pure.HashCodes.Benchmarks.Fakes;

internal sealed record FakeDeterminedHash : IDeterminedHash
{
    private readonly IEnumerable<byte> _hash =
    [
        130,
        69,
        151,
        1,
        3,
        139,
        193,
        122,
        182,
        30,
        13,
        221,
        74,
        60,
        6,
        86,
    ];

    public IEnumerator<byte> GetEnumerator()
    {
        return _hash.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
