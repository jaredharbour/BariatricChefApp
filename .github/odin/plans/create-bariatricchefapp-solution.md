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
