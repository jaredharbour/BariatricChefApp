# Active Context

## Current Focus
Initial solution scaffold complete. Solution structure, domain models, data layer, services, and API layer all created.

## Recent Changes
- Initialized git repo and created branch `odin/create-bariatricchefapp-solution`
- Scaffolded solution with `dotnet new sln`
- Created `BariatricChefApp.Core` (classlib) and `BariatricChefApp.Api` (webapi) projects
- Created `BariatricChefApp.Core.Tests` and `BariatricChefApp.Api.Tests` (xunit) projects
- Installed all NuGet packages (EF Core Cosmos, Microsoft.Identity.Web, Serilog, AutoMapper, Moq, FluentAssertions)
- Implemented domain models: `Recipe`, `Ingredient`, `NutritionInfo`, `UserProfile`, `BariatricStage` enum, `SurgeryType` enum
- Implemented repository interfaces and concrete implementations
- Implemented `BariatricChefDbContext` with CosmosDB entity configuration
- Implemented `UnitOfWork`
- Implemented `RecipeService`, `NutritionService`, `UserProfileService`
- Implemented API: `Program.cs`, 3 controllers, DTOs, AutoMapper profile, `GlobalExceptionMiddleware`
- Configured `appsettings.json` (production placeholders) and `appsettings.Development.json` (Cosmos emulator)

## Key Decisions Made
- CosmosDB Recipes partition key: `/stage` (not `/id`) — aligns with primary query pattern
- Auth: Azure AD B2C (not Entra ID workforce) — patients are external users
- Nutrition warnings are non-blocking (returned in response header, not 400 error)

## Next Steps
1. Add unit tests for `RecipeService` and `NutritionService`
2. Add integration tests with `WebApplicationFactory`
3. Provision Azure resources (Cosmos DB account, B2C tenant, App Service)
4. Consider adding a `[AllowAnonymous]` dev bypass or test auth setup
