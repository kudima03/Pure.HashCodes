using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.DateTime;
using Pure.Primitives.Abstractions.DayOfWeek;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Abstractions.Time;
using System.Collections;

namespace Pure.HashCodes;

public sealed record DeterminedHash : IDeterminedHash
{
    private readonly IDeterminedHash _hash;

    public DeterminedHash(IBool value) : this(new HashFromBool(value)) { }

    public DeterminedHash(IChar value) : this(new HashFromChar(value)) { }

    public DeterminedHash(ITime value) : this(new HashFromTime(value)) { }

    public DeterminedHash(IDate value) : this(new HashFromDate(value)) { }

    public DeterminedHash(IDayOfWeek value) : this(new HashFromDayOfWeek(value)) { }

    public DeterminedHash(IDateTime value) : this(new HashFromDateTime(value)) { }

    public DeterminedHash(INumber<double> value) : this(new HashFromDouble(value)) { }

    public DeterminedHash(INumber<float> value) : this(new HashFromFloat(value)) { }

    public DeterminedHash(INumber<int> value) : this(new HashFromInt(value)) { }

    public DeterminedHash(INumber<uint> value) : this(new HashFromUInt(value)) { }

    public DeterminedHash(INumber<ushort> value) : this(new HashFromUShort(value)) { }

    public DeterminedHash(IGuid value) : this(new HashFromGuid(value)) { }

    public DeterminedHash(IString value) : this(new HashFromString(value)) { }

    public DeterminedHash(IEnumerable<IDeterminedHash> hashes) : this(new AggregatedHash(hashes)) { }

    public DeterminedHash(IEnumerable<byte> hashBytes) : this(new HashFromBytes(hashBytes)) { }

    private DeterminedHash(IDeterminedHash hash)
    {
        _hash = hash;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return _hash.GetEnumerator();
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