# BariatricChef App — Project Brief

## Project Overview
BariatricChefApp is a recipe application designed specifically for people who have undergone bariatric surgery. These patients have strict, stage-based dietary requirements that evolve over weeks and months post-surgery.

## Core Problem
Bariatric surgery patients cannot eat standard recipes. They need:
- Stage-appropriate recipes (clear liquid → full liquid → pureed → soft → regular)
- Surgery-type filtering (gastric bypass patients risk dumping syndrome from sugar; sleeve patients have different tolerances)
- Strict portion control (2–8 oz per serving depending on stage)
- High protein, low sugar, low fat requirements

## Solution
A recipe API that tags recipes with allowed stages and compatible surgery types, then filters them for each patient based on their profile.

## Project Scope
- **In scope**: Recipe CRUD, stage/surgery-type filtering, per-patient profiles, nutritional validation, Azure AD B2C auth
- **Out of scope (v1)**: Meal planning, shopping lists, social/community features, mobile app

## Target Users
Post-bariatric surgery patients (external users, not employees). Azure AD B2C for identity.

## Success Criteria
- Patients can find recipes appropriate for their current stage and surgery type
- Nutritional warnings surface for non-compliant recipes
- API is secured — patients can only access their own profile
