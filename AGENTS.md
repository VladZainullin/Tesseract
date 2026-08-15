# Repository guidance

This file applies to the entire repository.

## Project structure

- `Tesseract.Contracts/` contains public OCR abstractions, enums, and contracts. It must not contain P/Invoke code.
- `Tesseract/` contains the managed Tesseract implementation, native declarations, iterators, renderers, monitors, and safe handles.
- `Leptonica.Contracts/` contains public image abstractions and enums.
- `Leptonica/` contains the managed Leptonica implementation, native declarations, library resolution, and safe handles.
- `tests/Tesseract.UnitTests/` contains tests that must run without native Tesseract or Leptonica installations.
- `tests/Tesseract.IntegrationTest/` contains tests that exercise real native libraries. Its assembly and root namespace are `Tesseract.IntegrationTests` despite the singular directory and project filename.

Keep public contracts and their concrete implementations synchronized. For example, a new public operation on `TesseractResultIterator` should normally also be represented by `ITesseractResultIterator`.

## Build configuration

- Production projects target `net7.0` through `Directory.Build.props`.
- Test projects target `net7.0`, `net8.0`, `net9.0`, and `net10.0`.
- Nullable reference types, all .NET analyzers, and warnings-as-errors are enabled.
- Package versions are managed centrally in `Directory.Packages.props`.
- `NuGet.config` is intentionally commented out. Do not uncomment, replace, or report it as a defect unless the user explicitly asks for that change.
- Because the repository-local NuGet configuration is intentionally disabled, prefer `--no-restore` for verification when restored assets are already present.

Use a serialized build when MSBuild workers become unreliable in this repository:

```sh
dotnet build Tesseract.slnx --no-restore -m:1 \
  -p:UseSharedCompilation=false \
  -p:BuildInParallel=false \
  -nr:false
```

The existing `net7.0` test target can produce end-of-support and transitive-package compatibility warnings. Do not hide new compiler or analyzer warnings under those known SDK/package warnings.

## Native interop rules

- Use source-generated `[LibraryImport]` declarations with the C calling convention and explicit UTF-8 marshalling where required.
- Prefer `SafeHandle` parameters over raw `nint` for managed-owned native objects. This keeps handles alive for the full P/Invoke call.
- Every owned native pointer must have one clear owner and exactly one matching release function.
- Use a dedicated `SafeHandle` for newly owned Tesseract and Leptonica objects.
- Validate native factory results before exposing them as managed objects. Reject null, closed, or invalid handles in public wrapper constructors.
- Distinguish owned strings from borrowed `const char*` values using the native C header:
  - copy borrowed strings with `Marshal.PtrToStringUTF8` and do not free them;
  - free allocated text exactly once with the matching Tesseract deletion function.
- When a native API returns a borrowed child pointer, keep its owner alive for at least as long as the managed child wrapper.
- Do not expose callback function pointers without rooting their delegates for the entire native callback lifetime.
- Validate sizes, dimensions, strides, enum values, and buffer lengths before passing managed memory to native code.
- Preserve ownership-transfer semantics. If an insert/chain operation transfers ownership, invalidate the previous managed owner and document that behavior.

## API design

- Public resource-owning types implement `IDisposable` and expose their safe handle through `IHasSafeHandle` or the relevant contract.
- Prefer `Try...` names only when failure is represented by `false`; invalid arguments and invalid native handles should throw.
- Keep nullability consistent between interfaces and implementations.
- Use argument guards before entering native code.
- Use file-scoped namespaces and follow the formatting already present in the surrounding project.
- Avoid exposing internal native declarations merely to make a feature public; add a managed method to the appropriate wrapper instead.

## Iterator and renderer lifetimes

- Text returned by `TessResultIteratorGetUTF8Text` is owned and must be released with `TessDeleteText` after conversion.
- Recognition-language, font-name, renderer-title, and renderer-extension pointers are borrowed and must not be released by managed code.
- Result, page, and choice iterators must not be used after their handle or the native state they depend on has been disposed or reset.
- Renderer-chain nodes returned from `TryNext` are borrowed from the root renderer chain; changes in this area must preserve the root lifetime.

## Tests

- Use TUnit for all tests.
- Put pure validation, marshalling, and safe-handle behavior in `Tesseract.UnitTests`.
- Put OCR, filesystem renderer output, native version checks, real PIX operations, and language-data behavior in `Tesseract.IntegrationTests`.
- Unit tests must not set `TESSERACT_LIBRARY_PATH`, load native libraries, or depend on Homebrew/system installation paths.
- Integration tests may use `TESSERACT_LIBRARY_PATH` and `TESSDATA_PREFIX`; keep environment discovery cross-platform and allow explicit values to take precedence.
- Native integration tests are marked non-parallel because native state and environment variables can interfere across tests.
- Add regression coverage for every ownership, marshalling, or native-lifetime bug.
- Keep temporary test artifacts under the system temporary directory and delete only the exact files created by the test.

Run TUnit executables for each target framework after building:

```sh
tests/Tesseract.UnitTests/bin/Debug/net10.0/Tesseract.UnitTests --progress off
tests/Tesseract.IntegrationTest/bin/Debug/net10.0/Tesseract.IntegrationTests --progress off
```

For changes to shared contracts or interop code, verify all four test target frameworks, not only `net10.0`.

## Change discipline

- Preserve unrelated working-tree changes.
- Do not edit generated `bin/` or `obj/` contents.
- Before finishing, run `git diff --check`, inspect `git status --short`, and report any verification limitations caused by missing native libraries or restored packages.
