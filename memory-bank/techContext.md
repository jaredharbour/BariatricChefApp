# Tech Context

## Stack
| Layer | Technology |
|-------|-----------|
| Runtime | .NET 8 LTS |
| API framework | ASP.NET Core 8 Web API |
| Database | Azure Cosmos DB (NoSQL) |
| ORM | EF Core 8 with Cosmos provider (`Microsoft.EntityFrameworkCore.Cosmos` 8.0.14) |
| Auth | Azure AD B2C via `Microsoft.Identity.Web` 3.8.2 |
| Logging | Serilog 9 with Console sink |
| API docs | Swashbuckle / OpenAPI 3 |
| Object mapping | AutoMapper 13 |
| Testing | xUnit, Moq, FluentAssertions |

## Dev Prerequisites
- .NET 8 SDK
- Azure Cosmos DB emulator (local dev) or Azure subscription
- Azure AD B2C tenant (or skip auth in dev by disabling `[Authorize]`)

## Build Commands
```bash
# Build solution
dotnet build

# Run tests
dotnet test

# Run API
cd src/BariatricChefApp.Api
dotnet run
```

## CosmosDB Local Dev
The `appsettings.Development.json` is pre-configured for the **Cosmos DB Emulator** (default endpoint + well-known key).
Install from: https://aka.ms/cosmosdb-emulator

## Configuration Required (Production)
1. `AzureAdB2C:Instance` — B2C login URL (e.g. `https://mytenant.b2clogin.com/`)
2. `AzureAdB2C:ClientId` — App registration client ID
3. `AzureAdB2C:Domain` — Tenant domain
4. `AzureAdB2C:SignUpSignInPolicyId` — B2C user flow name
5. `CosmosDb:Endpoint` — Cosmos account URI
6. `CosmosDb:Key` — Cosmos primary key
7. `CosmosDb:DatabaseName` — Database name (default: `BariatricChef`)

## Project Dependencies
```
BariatricChefApp.Api
  └── BariatricChefApp.Core

BariatricChefApp.Api.Tests
  ├── BariatricChefApp.Api
  └── BariatricChefApp.Core

BariatricChefApp.Core.Tests
  └── BariatricChefApp.Core
```
