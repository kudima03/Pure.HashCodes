namespace Pure.HashCodes.Internals.AggregatedHash;

internal sealed class DeterminedHashComparer : IComparer<IDeterminedHash>
{
    public int Compare(IDeterminedHash? first, IDeterminedHash? second)
    {
        if (ReferenceEquals(first, second))
        {
            return 0;
        }

        using IEnumerator<byte> enum1 = first!.GetEnumerator();
        using IEnumerator<byte> enum2 = second!.GetEnumerator();

        while (enum1.MoveNext() & enum2.MoveNext())
        {
            int cmp = enum1.Current.CompareTo(enum2.Current);
            if (cmp != 0)
            {
                return cmp;
            }
        }

        return enum1.MoveNext() ? 1 : enum2.MoveNext() ? -1 : 0;
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
