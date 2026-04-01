using BariatricChefApp.Core.Domain;
using BariatricChefApp.Core.Domain.Enums;
using BariatricChefApp.Core.Interfaces;

namespace BariatricChefApp.Core.Services;

public class RecipeService
{
    private readonly IUnitOfWork _unitOfWork;

    public RecipeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Recipe>> GetRecipesForPatientAsync(
        BariatricStage stage,
        SurgeryType? surgeryType = null,
        CancellationToken cancellationToken = default)
    {
        if (surgeryType.HasValue)
            return await _unitOfWork.Recipes.GetByStageAndSurgeryTypeAsync(stage, surgeryType.Value, cancellationToken);

        return await _unitOfWork.Recipes.GetByStageAsync(stage, cancellationToken);
    }

    public async Task<Recipe?> GetRecipeByIdAsync(string id, string stage, CancellationToken cancellationToken = default)
        => await _unitOfWork.Recipes.GetByIdAsync(id, stage, cancellationToken);

    public async Task<Recipe> CreateRecipeAsync(Recipe recipe, CancellationToken cancellationToken = default)
    {
        if (recipe.AllowedStages is null || recipe.AllowedStages.Count == 0)
            throw new ArgumentException("At least one allowed stage is required.", nameof(recipe));

        recipe.Id = Guid.NewGuid().ToString();
        recipe.CreatedAt = DateTime.UtcNow;
        recipe.UpdatedAt = DateTime.UtcNow;
        recipe.Stage = recipe.AllowedStages[0].ToString();

        await _unitOfWork.Recipes.AddAsync(recipe, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return recipe;
    }

    /// <param name="currentStage">The current partition key value of the recipe in CosmosDB.</param>
    public async Task<Recipe?> UpdateRecipeAsync(string id, string currentStage, Recipe updated, CancellationToken cancellationToken = default)
    {
        if (updated.AllowedStages is null || updated.AllowedStages.Count == 0)
            throw new ArgumentException("At least one allowed stage is required.", nameof(updated));

        var existing = await _unitOfWork.Recipes.GetByIdAsync(id, currentStage, cancellationToken);
        if (existing is null) return null;

        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.Instructions = updated.Instructions;
        existing.ServingSizeOz = updated.ServingSizeOz;
        existing.PrepTimeMinutes = updated.PrepTimeMinutes;
        existing.CookTimeMinutes = updated.CookTimeMinutes;
        existing.AllowedStages = updated.AllowedStages;
        existing.CompatibleSurgeryTypes = updated.CompatibleSurgeryTypes;
        existing.NutritionInfo = updated.NutritionInfo;
        existing.Ingredients = updated.Ingredients;
        existing.Tags = updated.Tags;
        existing.Stage = updated.AllowedStages[0].ToString();
        existing.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Recipes.UpdateAsync(existing, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteRecipeAsync(string id, string stage, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.Recipes.GetByIdAsync(id, stage, cancellationToken);
        if (existing is null) return false;

        await _unitOfWork.Recipes.DeleteAsync(id, stage, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
