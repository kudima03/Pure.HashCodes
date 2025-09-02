using System.Collections.Immutable;

namespace Pure.HashCodes.Internals.AggregatedHash;

internal sealed class DeterminedHashComparer : IComparer<IDeterminedHash>
{
    public int Compare(IDeterminedHash? first, IDeterminedHash? second)
    {
        if (ReferenceEquals(first, second))
        {
            return 0;
        }

        IImmutableList<byte> firstHashBytes = first!.ToImmutableArray();
        IImmutableList<byte> secondHashBytes = second!.ToImmutableArray();

        return firstHashBytes
            .Zip(
                secondHashBytes,
                (elementInFirst, elementInSecond) =>
                    elementInFirst.CompareTo(elementInSecond)
            )
            .FirstOrDefault(
                cmp => cmp != 0,
                firstHashBytes.Count.CompareTo(secondHashBytes.Count)
            );
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
