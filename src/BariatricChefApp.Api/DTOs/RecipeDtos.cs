using BariatricChefApp.Core.Domain.Enums;

namespace BariatricChefApp.Api.DTOs;

public record NutritionInfoDto(
    double Calories,
    double ProteinGrams,
    double FatGrams,
    double SugarGrams,
    double CarbGrams,
    double FiberGrams);

public record IngredientDto(
    string Id,
    string Name,
    double Quantity,
    string Unit,
    NutritionInfoDto? NutritionInfo);

public record RecipeDto(
    string Id,
    string Title,
    string Description,
    string Instructions,
    double ServingSizeOz,
    int PrepTimeMinutes,
    int CookTimeMinutes,
    List<BariatricStage> AllowedStages,
    List<SurgeryType> CompatibleSurgeryTypes,
    NutritionInfoDto NutritionInfo,
    List<IngredientDto> Ingredients,
    List<string> Tags,
    DateTime CreatedAt,
    DateTime UpdatedAt);

public record RecipeCreateRequest(
    string Title,
    string Description,
    string Instructions,
    double ServingSizeOz,
    int PrepTimeMinutes,
    int CookTimeMinutes,
    List<BariatricStage> AllowedStages,
    List<SurgeryType> CompatibleSurgeryTypes,
    NutritionInfoDto NutritionInfo,
    List<IngredientDto> Ingredients,
    List<string> Tags);

public record RecipeUpdateRequest(
    string Title,
    string Description,
    string Instructions,
    double ServingSizeOz,
    int PrepTimeMinutes,
    int CookTimeMinutes,
    List<BariatricStage> AllowedStages,
    List<SurgeryType> CompatibleSurgeryTypes,
    NutritionInfoDto NutritionInfo,
    List<IngredientDto> Ingredients,
    List<string> Tags);
