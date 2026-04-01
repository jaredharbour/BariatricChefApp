# System Patterns

## Solution Structure
```
BariatricChefApp.sln
├── src/
│   ├── BariatricChefApp.Api        # ASP.NET Core 8 Web API
│   └── BariatricChefApp.Core       # Class Library — business + data
└── tests/
    ├── BariatricChefApp.Api.Tests
    └── BariatricChefApp.Core.Tests
```

## Architecture Decisions

### Repository + Unit of Work
- `IRecipeRepository`, `IUserProfileRepository` → concrete implementations in `Core/Data/Repositories/`
- `IUnitOfWork` aggregates both repos; services take `IUnitOfWork` as their only data dependency
- `UnitOfWork` is registered as scoped; lazy-initializes repos on first access

### CosmosDB Partition Strategy
- **Recipes container**: partitioned by `/stage` (string value of `BariatricStage` enum)
  - Rationale: Primary read path is "all recipes for Stage X" — partition-local query
  - `Recipe.Stage` property is set to `AllowedStages.First().ToString()` at creation time
- **UserProfiles container**: partitioned by `/userId` (Azure AD B2C object ID)
  - Each user's profile is a single document — partition key = lookup key

### Auth Pattern
- Azure AD B2C (Entra External ID) — patient-facing external users
- `Microsoft.Identity.Web` with `AddMicrosoftIdentityWebApi` targeting `AzureAdB2C` config section
- User identity extracted from `sub` claim or `ClaimTypes.NameIdentifier`
- All endpoints require `[Authorize]`

### Service Layer
- Services (`RecipeService`, `NutritionService`, `UserProfileService`) live in `Core/Services/`
- Services are registered in `CoreServiceExtensions.AddBariatricChefCore()` for clean DI setup
- `NutritionService` is pure (no DB access) — validates recipes against hardcoded stage guidelines

### DTO / Mapping Pattern
- DTOs are C# records in `Api/DTOs/`
- AutoMapper `MappingProfile` in `Api/Mapping/` handles all conversions
- API layer never exposes domain models directly

### Error Handling
- `GlobalExceptionMiddleware` catches all unhandled exceptions
- Returns RFC 7807 `ProblemDetails` JSON responses
- Exception type → HTTP status code mapping: `ArgumentException → 400`, `KeyNotFoundException → 404`, `UnauthorizedAccessException → 403`

## Key Conventions
- Async/await throughout — all repository and service methods are async
- `CancellationToken` passed through all async methods
- DateTime always UTC (`DateTime.UtcNow`)
- CosmosDB `id` field maps to `Id` (string GUID) on all entities
