# batch-job-automation-mock

**Last Updated:** 2025-10-16

## 🧩 Overview
**batchJob-automation-mock** is a **.NET 8.0 xUnit test suite** built to validate batchJob-processing logic using deterministic mock data files of Azure jobs log.  
It ensures consistent and repeatable test runs both locally and in CI.

---

## ⚙️ Tech Stack

| Category | Details |
|-----------|----------|
| **Language / Runtime** | C# / .NET 8.0 |
| **Test Framework** | xUnit 2.5.0 |
| **Test Runner / SDK** | Microsoft.NET.Test.Sdk 17.14.1 + xunit.runner.visualstudio 2.5.0 |
| **Coverage Tool** | coverlet.collector 6.0.0 (Cobertura) |
| **CI/CD** | GitHub Actions |
| **IDE Support** | Visual Studio 2022 / VS Code / JetBrains Rider |
| **OS Targets** | Windows, Linux, macOS |

---

## 🗂️ Project Structure

```
BatchValidatorMockTest.sln
└─ tests/BatchValidatorMockTest/
   ├─ BatchValidatorMockTest.csproj
   ├─ Helpers/
   │  └─ MockHelper.cs
   ├─ ResponseFile/
   │  ├─ Job1mockSuccess.json
   │  ├─ Job1mockFailure.json
   │  ├─ Job1mockSuccess.txt
   │  └─ Job1mockFailure.txt
   └─ Tests/
      └─ BatchValidatorTests.cs
```

- **ResponseFile/** → static test assets copied to output for deterministic tests.
- **Helpers/** → utility functions (e.g., `MockHelper` for reading files safely).
- **Tests/** → all xUnit test classes following Arrange–Act–Assert style.

---

## 🧪 Testing Pattern

Example usage:
```csharp
var path = Path.Combine(AppContext.BaseDirectory, "ResponseFile", "Job1", "Job1mockSuccess.json");
var mock = MockHelper.LoadMockResponse(path);
Assert.Contains("ExpectedValue", mock);
```

**Guidelines**
- Keep all mock data inside `ResponseFile/`.
- Use `AppContext.BaseDirectory` and `Path.Combine` for cross-platform safety.
- Name tests clearly: `MethodName_Should_ExpectedBehavior_When_Condition`.

---

## 🚀 CI Pipeline (GitHub Actions)

Workflow file: `.github/workflows/ci.yml`

### Jobs
1. **build**  
   - Restores and builds the solution.
2. **BatchJobTestResults**  
   - Runs tests with TRX + Cobertura coverage.
   - Publishes summary and uploads artifacts.

### Artifacts
- `test-results.trx`
- `coverage.cobertura.xml`

### Test Summary
✅ Total / Passed / Failed / Skipped chart visible in **Actions → Checks** tab.

---

## 🧾 Developer Workflow

### Local commands
```bash
dotnet restore
dotnet build
dotnet test --collect:"XPlat Code Coverage"
```

### CI triggers
- Runs on every **push** or **pull request**.

---

## 🔒 Repository Hygiene

- `.gitignore` excludes:
  ```
  **/bin/
  **/obj/
  TestResults/
  coverage/
  .vs/
  ```
- Keeps build artifacts out of Git.
- Only tracks test source and assets.

---

## 📊 Coverage Report (optional)

Generate an HTML report locally:
```bash
reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:coverage-report
```

---

## 🏗️ Badges

Add a CI badge to your README for quick status visibility:
```markdown
![CI](https://github.com/<your-username>/<repo-name>/actions/workflows/ci.yml/badge.svg)
```

---

## 🧠 Summary

> **batch-automation-mock** is a clean, cross-platform .NET 8 test project validating batch-processing behavior through deterministic mocks.  
> Fully automated with GitHub Actions, it provides coverage metrics and a professional, zero-noise repository setup.



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