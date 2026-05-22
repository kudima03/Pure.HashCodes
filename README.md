# Pure.HashCodes

Deterministic hash code generation for the **Pure** ecosystem — stable, byte-enumerable hashes over immutable primitive types.

[![.NET build & test](https://github.com/kudima03/Pure.HashCodes/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.HashCodes/actions/workflows/build-and-test.yml)
[![Benchmarks](https://github.com/kudima03/Pure.HashCodes/actions/workflows/Benchmarks.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.HashCodes/actions/workflows/Benchmarks.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.HashCodes/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.HashCodes/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.HashCodes)](https://www.nuget.org/packages/Pure.HashCodes)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE.txt)

## Overview

`Pure.HashCodes` provides a single entry point — `DeterminedHash` — that produces a stable, ordered sequence of bytes for any value implementing a `Pure.Primitives.Abstractions` interface. Unlike `object.GetHashCode()`, the output is deterministic across processes and runtimes and can be combined from multiple fields into a single compound hash.

`GetHashCode()` and `ToString()` are intentionally unsupported on `DeterminedHash` and throw `NotSupportedException` — callers must consume the byte sequence directly.

## API

`DeterminedHash` is a `sealed record` implementing `IDeterminedHash : IEnumerable<byte>`.

### Constructors

| Constructor | Input type |
|---|---|
| `DeterminedHash(IBool)` | Boolean value |
| `DeterminedHash(IChar)` | Single character |
| `DeterminedHash(IDate)` | Date (day / month / year) |
| `DeterminedHash(ITime)` | Time (hour / minute / second / …) |
| `DeterminedHash(IDateTime)` | Date + time composition |
| `DeterminedHash(IDayOfWeek)` | Day of week |
| `DeterminedHash(INumber<double>)` | 64-bit floating point |
| `DeterminedHash(INumber<float>)` | 32-bit floating point |
| `DeterminedHash(INumber<int>)` | Signed 32-bit integer |
| `DeterminedHash(INumber<uint>)` | Unsigned 32-bit integer |
| `DeterminedHash(INumber<ushort>)` | Unsigned 16-bit integer |
| `DeterminedHash(IGuid)` | GUID value |
| `DeterminedHash(IString)` | String value |
| `DeterminedHash(IEnumerable<IDeterminedHash>)` | Aggregated compound hash |
| `DeterminedHash(IEnumerable<byte>)` | Raw byte sequence |

## Design Principles

- **Deterministic** — identical inputs always produce identical byte sequences, regardless of runtime or process lifetime.
- **Abstraction-driven** — works exclusively with `Pure.Primitives.Abstractions` interfaces, not concrete types.
- **Composable** — multiple `DeterminedHash` values can be aggregated into a single compound hash via `IEnumerable<IDeterminedHash>`.
- **AOT-compatible** — the library is fully compatible with Native AOT compilation.

## Dependencies

- [`Pure.HashCodes.Abstractions`](https://github.com/kudima03/Pure.HashCodes.Abstractions/tree/0.2.0) — defines `IDeterminedHash : IEnumerable<byte>`, the core hash abstraction
- [`Pure.Primitives.Abstractions`](https://github.com/kudima03/Pure.Primitives.Abstractions/tree/4.3.0) — read-only interfaces for primitive types (`IBool`, `IChar`, `IDate`, `ITime`, `IDateTime`, `IDayOfWeek`, `INumber<T>`, `IGuid`, `IString`)

## Target Frameworks

- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Installation

```shell
dotnet add package Pure.HashCodes
```

## Usage

Hash a single primitive:

```csharp
using Pure.HashCodes;
using Pure.Primitives.Number;

INumber<int> id = new Int(42);
IEnumerable<byte> hashBytes = new DeterminedHash(id);
```

Combine multiple fields into a compound hash:

```csharp
using Pure.HashCodes;
using Pure.HashCodes.Abstractions;

IDeterminedHash nameHash = new DeterminedHash(name);
IDeterminedHash dateHash = new DeterminedHash(birthDate);

IEnumerable<byte> compound = new DeterminedHash(new[] { nameHash, dateHash });
```
