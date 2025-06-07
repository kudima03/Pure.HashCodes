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
    private readonly IEnumerable<byte> _hashBytes;

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

    private DeterminedHash(IEnumerable<byte> hashBytes)
    {
        _hashBytes = hashBytes;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return _hashBytes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}