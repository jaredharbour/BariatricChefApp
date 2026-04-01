using BariatricChefApp.Api.DTOs;
using BariatricChefApp.Core.Domain;

namespace BariatricChefApp.Api.Mapping;

public static class RecipeMappingExtensions
{
    public static RecipeDto ToDto(this Recipe recipe) => new(
        Id: recipe.Id,
        Title: recipe.Title,
        Description: recipe.Description,
        Instructions: recipe.Instructions,
        ServingSizeOz: recipe.ServingSizeOz,
        PrepTimeMinutes: recipe.PrepTimeMinutes,
        CookTimeMinutes: recipe.CookTimeMinutes,
        AllowedStages: recipe.AllowedStages,
        CompatibleSurgeryTypes: recipe.CompatibleSurgeryTypes,
        NutritionInfo: recipe.NutritionInfo.ToDto(),
        Ingredients: recipe.Ingredients.Select(i => i.ToDto()).ToList(),
        Tags: recipe.Tags,
        CreatedAt: recipe.CreatedAt,
        UpdatedAt: recipe.UpdatedAt);

    public static IReadOnlyList<RecipeDto> ToDtoList(this IReadOnlyList<Recipe> recipes) =>
        recipes.Select(r => r.ToDto()).ToList();

    public static Recipe ToDomain(this RecipeCreateRequest request) => new()
    {
        Title = request.Title,
        Description = request.Description,
        Instructions = request.Instructions,
        ServingSizeOz = request.ServingSizeOz,
        PrepTimeMinutes = request.PrepTimeMinutes,
        CookTimeMinutes = request.CookTimeMinutes,
        AllowedStages = request.AllowedStages,
        CompatibleSurgeryTypes = request.CompatibleSurgeryTypes,
        NutritionInfo = request.NutritionInfo.ToDomain(),
        Ingredients = request.Ingredients.Select(i => i.ToDomain()).ToList(),
        Tags = request.Tags
    };

    public static Recipe ToDomain(this RecipeUpdateRequest request) => new()
    {
        Title = request.Title,
        Description = request.Description,
        Instructions = request.Instructions,
        ServingSizeOz = request.ServingSizeOz,
        PrepTimeMinutes = request.PrepTimeMinutes,
        CookTimeMinutes = request.CookTimeMinutes,
        AllowedStages = request.AllowedStages,
        CompatibleSurgeryTypes = request.CompatibleSurgeryTypes,
        NutritionInfo = request.NutritionInfo.ToDomain(),
        Ingredients = request.Ingredients.Select(i => i.ToDomain()).ToList(),
        Tags = request.Tags
    };

    private static NutritionInfoDto ToDto(this NutritionInfo n) => new(
        Calories: n.Calories,
        ProteinGrams: n.ProteinGrams,
        FatGrams: n.FatGrams,
        SugarGrams: n.SugarGrams,
        CarbGrams: n.CarbGrams,
        FiberGrams: n.FiberGrams);

    private static NutritionInfo ToDomain(this NutritionInfoDto n) => new()
    {
        Calories = n.Calories,
        ProteinGrams = n.ProteinGrams,
        FatGrams = n.FatGrams,
        SugarGrams = n.SugarGrams,
        CarbGrams = n.CarbGrams,
        FiberGrams = n.FiberGrams
    };

    private static IngredientDto ToDto(this Ingredient i) => new(
        Id: i.Id,
        Name: i.Name,
        Quantity: i.Quantity,
        Unit: i.Unit,
        NutritionInfo: i.NutritionInfo?.ToDto());

    private static Ingredient ToDomain(this IngredientDto i) => new()
    {
        Id = i.Id,
        Name = i.Name,
        Quantity = i.Quantity,
        Unit = i.Unit,
        NutritionInfo = i.NutritionInfo?.ToDomain()
    };
}
