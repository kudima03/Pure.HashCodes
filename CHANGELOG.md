# Changelog

All notable changes to Pure.HashCodes are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [2.1.0] — 2025-12-05

### Added
- Package now multi-targets `net7.0`, `net8.0`, `net9.0`, and `net10.0`
  (previously `net9.0` only).

## [2.0.0] — 2025-11-01

### Changed
- **Breaking:** `IDeterminedHash` moved out of `Pure.HashCodes` into the new
  `Pure.HashCodes.Abstractions` package. Consumers referencing the interface
  directly must add a reference to `Pure.HashCodes.Abstractions`.

### Removed
- **Breaking:** `AggregatedHash` is internal again and no longer part of the
  public API.

## [1.0.0] — 2025-08-25

### Fixed
- Hashing an empty byte collection (via `DeterminedHash(IEnumerable<byte>)`)
  no longer throws `ArgumentException`; it now returns the SHA-256 hash of
  the empty collection.

## [0.4.0] — 2025-06-23

### Added
- **`DeterminedHash(IEnumerable<byte>)`** — new public constructor for
  hashing raw byte sequences directly.

## [0.3.0] — 2025-06-16

### Added
- **`AggregatedHash(params IDeterminedHash[])`** — new params constructor
  overload for combining hashes without building a collection first.

## [0.2.0] — 2025-06-08

### Added
- **`AggregatedHash`** — made public (previously internal); can now be
  constructed directly to combine multiple `IDeterminedHash` instances.

## [0.1.0] — 2025-06-07

### Added
- Initial release.
- **`DeterminedHash`** — deterministic, byte-enumerable hash for values from
  the `Pure.Primitives.Abstractions` ecosystem: `IBool`, `IChar`, `IDate`,
  `IDateTime`, `IDayOfWeek`, `IGuid`, `IString`, `ITime`, and numeric types
  `INumber<double>`, `INumber<float>`, `INumber<int>`, `INumber<uint>`,
  `INumber<ushort>`. Also accepts `IEnumerable<IDeterminedHash>` to combine
  multiple hashes into one.
- **`IDeterminedHash`** — base interface (`IEnumerable<byte>`) implemented
  by every hash type.
