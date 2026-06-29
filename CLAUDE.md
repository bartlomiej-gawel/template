# CLAUDE.md

Clean Architecture solution template targeting **.NET 10** with a Blazor WebAssembly client, a minimal-API backend, and TUnit-based tests.
The codebase is currently scaffolding — projects and references exist, but most contain no domain code yet.

## Commands

```bash
dotnet build                              # build whole solution (Template.slnx)
dotnet test                               # run all test projects (Microsoft.Testing.Platform)
dotnet test tests/Template.Tests.Unit     # run a single test project
dotnet run --project src/Template.Api     # run the API
dotnet run --project src/Template.Client  # run the Blazor WASM client
docker compose up                         # build & run the API container (compose.yaml)
```

- SDK is pinned via `global.json` (`10.0.300`, no prerelease). 
- The test runner is **Microsoft.Testing.Platform** (configured in `global.json`), not VSTest — test projects are `OutputType=Exe`.
- `TreatWarningsAsErrors=true` is set globally, so builds fail on any warning.

## Architecture

Dependency direction (a project may only reference those listed):

| Project                   | References             | Role                                                    |
|---------------------------|------------------------|---------------------------------------------------------|
| `Template.Domain`         | —                      | Entities, value objects, domain logic. No outward deps. |
| `Template.Application`    | Domain                 | Use cases / application logic.                          |
| `Template.Infrastructure` | Application            | Persistence, external services.                         |
| `Template.Contracts`      | —                      | Shared DTOs across the API/client boundary.             |
| `Template.Api`            | Application, Contracts | Minimal-API host (`Microsoft.NET.Sdk.Web`).             |
| `Template.Client`         | Contracts              | Blazor WebAssembly SPA.                                 |

Keep the dependency rule strict: Domain stays dependency-free; the client and API share only `Contracts`.
`Template.Tests.Architecture` (ArchUnitNET) is intended to enforce these boundaries — update its rules when adding projects.

## Conventions

- **Central Package Management**: all package versions live in `Directory.Packages.props`. Reference packages in csproj **without** a `Version` attribute; add new versions to that file.
- Shared MSBuild settings (`net10.0`, `ImplicitUsings`, `Nullable`, warnings-as-errors) live in `Directory.Build.props` — don't duplicate them per project.
- Tests use **TUnit** (`TUnit.Mocks` for mocking). Integration tests use **Testcontainers** (PostgreSQL, Redis). Client tests use **bUnit**.
- UI is a **Blazor WebAssembly** SPA (`Template.Client`); no component library is currently referenced (to be selected).
