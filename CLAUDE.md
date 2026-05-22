# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror /p:RunAnalyzers=true
dotnet format --verify-no-changes                          # check code style (CI enforces this)
dotnet format                                              # auto-fix code style
dotnet test --no-build --verbosity normal --logger trx --collect:"XPlat Code Coverage"
dotnet pack --configuration Release -p:PackageVersion=<version> --output .
```

To run benchmarks (from `./src/Tests/Pure.HashCodes.Benchmarks`):

```bash
dotnet run --configuration Release
```

## Architecture

This is a **NuGet library** providing deterministic, byte-enumerable hash codes for primitive types from the Pure ecosystem.

**Public API — one type:**
- `DeterminedHash` (`sealed record`) — implements `IDeterminedHash : IEnumerable<byte>`. Accepts any `Pure.Primitives.Abstractions` interface type via overloaded constructors, plus `IEnumerable<IDeterminedHash>` for compound hashes and `IEnumerable<byte>` for raw bytes. `GetHashCode()` and `ToString()` intentionally throw `NotSupportedException`.

**Internal structure:**
- `Pure.HashCodes.Internals/` — one internal sealed class per primitive type (`HashFromBool`, `HashFromChar`, `HashFromDate`, etc.), each implementing `IDeterminedHash`.
- `Pure.HashCodes.Internals/AggregatedHash/` — `AggregatedHash` (combines multiple `IDeterminedHash`), `DeterminedHashComparer` (byte-by-byte comparison), `OrderedHashes`.

**Dependencies on Pure packages:**
- `Pure.HashCodes.Abstractions` supplies the `IDeterminedHash` interface — the return type of every hash operation.
- `Pure.Primitives.Abstractions` supplies the input interfaces (`IBool`, `IChar`, `IDate`, `ITime`, `IDateTime`, `IDayOfWeek`, `INumber<T>`, `IGuid`, `IString`) — `DeterminedHash` constructors accept only these abstractions, never concrete types.

**Multi-targeting:** `net7.0;net8.0;net9.0;net10.0`. All code must remain AOT-compatible (`IsAotCompatible = true`).

**Package validation:** `EnablePackageValidation = true` with `PackageValidationBaselineVersion = 2.1.0`. Breaking API changes fail the build.

**Publishing:** triggered by pushing a semver tag (e.g. `2.2.0`). The tag value becomes the `PackageVersion`. Packages are pushed to both GitHub Packages and NuGet.org.

**Tests:** xunit, targeting `net10.0`. Internal types are visible to `Pure.HashCodes.Tests` via `InternalsVisibleTo`. CI enforces ≥ 98% code coverage.

**Benchmarks:** BenchmarkDotNet, targeting `net10.0` (output executable). CI runs on a self-hosted `benchmarks` runner and fails if any benchmark regresses beyond 110% of the cached baseline.

## Code Style

Enforced via `.editorconfig` and `dotnet format --verify-no-changes` in CI:

- No `var` — always use explicit types.
- No expression-bodied methods, constructors, operators, or local functions — use block bodies.
- Properties, indexers, and accessors should use expression bodies where possible.
- Private fields: `_camelCase` prefix.
- File-scoped namespaces (`namespace Foo;` not `namespace Foo { }`).
- All `using` directives outside the namespace.
- Always use braces for code blocks — no single-line `if`/`for`/etc.
- Max line length: 90 characters.
- Explicit accessibility modifiers required on all members.

## Commit Messages

Do not mention Claude or AI assistance in commit messages.
