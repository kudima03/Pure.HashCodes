using System.Collections;
using System.Security.Cryptography;

namespace Pure.HashCodes;

public sealed record AggregatedHash : IDeterminedHash
{
    private readonly IEnumerable<IDeterminedHash> _hashes;

    public AggregatedHash(params IDeterminedHash[] hashes) : this(hashes.AsReadOnly()) { }

    public AggregatedHash(IEnumerable<IDeterminedHash> hashes)
    {
        _hashes = hashes;
    }

    private byte[] HashBytes
    {
        get
        {
            if (!_hashes.Any())
            {
                throw new ArgumentException();
            }

            IOrderedEnumerable<IDeterminedHash> orderedHashes = _hashes.Order(new LexicographicalComparisonRule());

            using SHA256 targetHash = SHA256.Create();

            foreach (byte[] hashBytes in orderedHashes.Select(hash => hash.ToArray()))
            {
                targetHash.TransformBlock(hashBytes, 0, hashBytes.Length, hashBytes, 0);
            }

            targetHash.TransformFinalBlock([], 0, 0);

            return targetHash.Hash!;
        }
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return HashBytes.AsEnumerable().GetEnumerator();
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

    private sealed record LexicographicalComparisonRule : IComparer<IDeterminedHash>
    {
        public int Compare(IDeterminedHash? first, IDeterminedHash? second)
        {
            return first!.Zip(second!, (firstByte, secondByte) => firstByte.CompareTo(secondByte))
                    .FirstOrDefault(comparisonResult => comparisonResult != 0, defaultValue: 0)
                switch
                {
                    0 => first!.Count().CompareTo(second!.Count()),
                    var comparisonResult => comparisonResult
                };
        }
    }
}