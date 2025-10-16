# BatchValidatorMockTest

A focused **xUnit** test project targeting **.NET 8.0** for validating batch-processing logic with deterministic fixtures. 
The solution uses a simple `BatchValidatorMockTest.sln` with one or more test projects. Test assets under `ResponseFile/` are copied to the test output for easy access during runs.

> _Updated on 2025-10-16_

## ✨ Features

- **.NET 8.0** test project (`net8.0`) with nullable reference types enabled
- **xUnit 2.5.0** with **Microsoft.NET.Test.Sdk 17.14.1** and **xunit.runner.visualstudio 2.5.0**
- Test data in `ResponseFile/**` is **always copied** to the output directory
- Works with **Visual Studio 2022** (latest) and **`dotnet` CLI**
- CI-friendly via `dotnet test` with TRX output

## 🧰 Tech Stack

- **Target Framework**: `net8.0`
- **SDK**: .NET SDK **8.0.x**
- **Test Framework**: xUnit **2.5.0**
- **Runner**: `xunit.runner.visualstudio` **2.5.0**
- **Test SDK**: `Microsoft.NET.Test.Sdk` **17.14.1**

## 📦 Prerequisites

- Install the **.NET 8 SDK**.
- (Optional) Visual Studio 2022 (latest) with the **.NET** workload.

Check your SDK:
```bash
dotnet --info
```

## 🗂️ Project Structure

```
BatchValidatorMockTest.sln
└─ tests/BatchValidatorMockTest/
   ├─ BatchValidatorMockTest.csproj
   ├─ ResponseFile/
   │  ├─ ... test payloads (json, txt, csv)
   └─ UnitTests/
      └─ *.cs
```

### `ResponseFile/` assets

The csproj includes:
```xml
<ItemGroup>
  <None Update="ResponseFile\**\*">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

At test runtime, files will be available under the test's output directory. You can read them like this:

```csharp
using System.IO;

string Root() => AppContext.BaseDirectory; // bin/Debug/net8.0/

string ReadResponse(string fileName)
{
    var path = Path.Combine(Root(), "ResponseFile", fileName);
    return File.ReadAllText(path);
}
```

> Tip: Prefer `AppContext.BaseDirectory` over relative paths so the code works in both CLI and Visual Studio runners.

## 🚀 Quick Start

Restore, build, and test:

```bash
# Restore
dotnet restore ./BatchValidatorMockTest.sln

# Build (Debug/Release)
dotnet build ./BatchValidatorMockTest.sln -c Debug

# Run tests with console + TRX
dotnet test ./BatchValidatorMockTest.sln -c Debug \
  --logger "console;verbosity=normal" \
  --logger "trx;LogFileName=test-results.trx"
```

Open in Visual Studio:
1. Open `BatchValidatorMockTest.sln`
2. Use **Test Explorer** → **Run All Tests**
3. Inspect `Test Results` for outputs and TRX artifacts

## ✅ Test Conventions

- Use **Arrange–Act–Assert** with clear naming:
  - `MethodName_Should_ExpectedBehavior_When_Condition()`
- Keep tests **isolated** (no network). Use `ResponseFile/` for deterministic inputs.
- If you need additional assets, drop them under `ResponseFile/` — they’ll be copied automatically.

## 📊 Code Coverage (optional)

With Coverlet collector:
```bash
dotnet test ./BatchValidatorMockTest.sln \
  /p:CollectCoverage=true \
  /p:CoverletOutput=./coverage/ \
  /p:CoverletOutputFormat=opencover
```

Generate HTML (with reportgenerator installed):
```bash
reportgenerator -reports:coverage/coverage.opencover.xml -targetdir:coverage-report
```

## 🔧 Sample CI (GitHub Actions)

```yaml
name: ci
on:
  push:
  pull_request:
jobs:
  test:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: "8.0.x"
      - run: dotnet restore ./BatchValidatorMockTest.sln
      - run: dotnet build ./BatchValidatorMockTest.sln -c Release --no-restore
      - run: dotnet test ./BatchValidatorMockTest.sln -c Release --no-build --logger "trx;LogFileName=test-results.trx"
```

## 🧪 Troubleshooting

- **Tests not discovered**: Ensure package versions match the csproj (xUnit 2.5.0, Test SDK 17.14.1, runner 2.5.0).
- **File not found**: Confirm assets live under `ResponseFile/` and verify they exist in `bin/<Config>/net8.0/ResponseFile/` after build.
- **SDK mismatch**: Verify `dotnet --info` reports an 8.0.x SDK.
- **Path separators**: Use `Path.Combine` rather than hardcoding `\\` or `/`.

## 📜 License

Specify your license (e.g., MIT).


## Ignore unwanted file
  Simple 3-Step Permanent Fix

1️⃣ Make sure the .gitignore is in the root of your repo

AutomationSuite/.gitignore
AutomationSuite/AutomationSuite.sln

2️⃣ Tell Git to re-index using the ignore rules

cd AutomationSuite
git rm -r --cached .
git add .
git commit -m "Apply .gitignore and re-index project"

3️⃣ Verify
git status

Now rebuild:

dotnet clean
dotnet build

→ The build output (bin/, obj/, TestResults/, Screenshots/, etc.) will remain on disk but will no longer appear as “Changes” in Git.