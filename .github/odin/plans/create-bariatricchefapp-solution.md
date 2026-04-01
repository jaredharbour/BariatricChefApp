# create-bariatricchefapp-solution

**Date**: 2026-04-01  
**Size**: Large  
**Risk**: 🟢

## Plan
Create a new .NET 8 solution called BariatricChefApp with two source projects (Api + Core) and two xUnit test projects. Domain covers bariatric surgery dietary stages, surgery types, and recipe nutritional validation. CosmosDB via EF Core with Repository + UoW pattern. Azure AD B2C auth for patient-facing external users.

## Frigg Review
Frigg (gpt-5.4) raised two concerns:
1. Recipes partition key `/id` → changed to `/stage` for efficient stage-based queries
2. Azure AD Entra ID workforce auth → changed to Azure AD B2C for external patients
Both resolved autonomously before implementation.

## Completion

**Commit**: b4e8b70  
**Branch**: odin/create-bariatricchefapp-solution  
**Build**: ✅ 0 errors, 0 warnings  
**Tests**: ✅ 2/2 passed  

### Adversarial Review Summary (5 reviewers)

| Reviewer | Model | Findings |
|---|---|---|
| Mimir | gpt-5.3-codex | 2 must-fix, 3 should-fix |
| Tyr | gpt-5.3-codex | 2 critical, 2 major, 1 minor |
| Heimdall | gpt-5.3-codex | 3 high, 3 medium |
| Thor | gpt-5.4 | 3 high, 1 medium |
| Loki | claude-opus-4.6 | 1 critical, 4 high, 2 medium |

### Fixes Applied

- `CreateRecipeAsync`: Guard empty `AllowedStages` → throw `ArgumentException` (was silent `PreOp` partition)
- `UpdateRecipeAsync`: Added `currentStage` param; lookup now uses stored partition key, not request body
- `UpdateRecipeAsync`: Recomputes `existing.Stage` from updated `AllowedStages[0]`
- `RecipesController.UpdateRecipe`: Added `[FromQuery] string stage` param passed to service
- `RecipesController.DeleteRecipe`: Ownership check — compare `CreatedByUserId` vs authenticated user
- `RecipesController.UpdateRecipe`: Ownership check added
- `RecipesController.GetRecipes`: `BariatricStage stage` → nullable `BariatricStage? stage` with explicit 400
- `RecipesController.CreateRecipe`: `GetUserId()` helper (consistent with `UserProfileController`)
- `GlobalExceptionMiddleware`: `OperationCanceledException` short-circuits (no 500, no response write)
- `GlobalExceptionMiddleware`: `HasStarted` guard before writing error response
- `GlobalExceptionMiddleware`: `ex.Message` redacted for 500s in production (`IHostEnvironment`)
- `.http` + `launchSettings.json`: Removed stale `weatherforecast` scaffold references
