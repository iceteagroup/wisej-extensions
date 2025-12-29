# Repository Guidelines

## Project Structure & Module Organization
This repository is a multi-project .NET solution for Wisej standard extensions.
- `Wisej.Extensions.sln` is the entry solution for all extensions.
- Extension projects live under `Wisej.Ext.*` and `Wisej.Web.Ext.*` (one folder per extension).
- Shared build settings live in `Directory.Build.props`; SDK pinning is in `global.json`.
- Packaging assets commonly include `*.nuspec` files and `Resources/` folders inside each extension.
- CI packaging stages outputs into `Distro/` and `artifacts/` (see `.github/workflows/build-and-pack.yml`).

## Build, Test, and Development Commands
Run these from the repo root:
- `dotnet workload restore Wisej.Extensions.sln` to install required workloads (matches CI).
- `dotnet restore Wisej.Extensions.sln` to restore NuGet dependencies.
- `dotnet build Wisej.Extensions.sln -c Release` to build all extensions.
- `dotnet build Wisej.Ext.PlayWright/Wisej.Ext.PlayWright/Wisej.Ext.PlayWright.csproj -c Release` for the PlayWright extension.
- `dotnet test Wisej.Ext.PlayWright/Wisej.Ext.Playwright.Test/Wisej.Ext.Playwright.Test.csproj` to run Playwright-based tests.
- `dotnet test Wisej.Ext.PlayWright/Wisej.Automation.NUnit.Test/Wisej.Automation.NUnit.Test.csproj` for the NUnit automation tests.

## Coding Style & Naming Conventions
- C# code uses tabs for indentation and braces on new lines (match existing files).
- Public types and members use PascalCase; local variables use camelCase.
- Namespaces and folders follow `Wisej.Ext.*` and `Wisej.Web.Ext.*` patterns.
- Keep icons and other static assets under the extension’s `Resources/` directory.

## Testing Guidelines
- Tests are primarily in the PlayWright automation projects under `Wisej.Ext.PlayWright/`.
- Keep UI/automation tests focused and isolated; any generated screenshots live under `Wisej.Ext.PlayWright/Wisej.Ext.Playwright.Test/reports/`.
- If adding tests, follow the existing NUnit attribute style (`[Test]`).

## Commit & Pull Request Guidelines
- Use concise, imperative commit messages (e.g., `Update build-and-pack.yml`, `Fix packaging path`).
- Automation may use prefixes like `chore:` for version bumps; mirror that when appropriate.
- PRs should describe impacted extensions, link related issues, and include screenshots for UI changes.
