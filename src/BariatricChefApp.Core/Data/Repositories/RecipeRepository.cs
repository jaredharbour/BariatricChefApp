using BariatricChefApp.Core.Domain;
using BariatricChefApp.Core.Domain.Enums;
using BariatricChefApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BariatricChefApp.Core.Data.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly BariatricChefDbContext _context;

    public RecipeRepository(BariatricChefDbContext context)
    {
        _context = context;
    }

    public async Task<Recipe?> GetByIdAsync(string id, string stage, CancellationToken cancellationToken = default)
    {
        return await _context.Recipes
            .WithPartitionKey(stage)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Recipe>> GetByStageAsync(BariatricStage stage, CancellationToken cancellationToken = default)
    {
        var stageKey = stage.ToString();
        return await _context.Recipes
            .WithPartitionKey(stageKey)
            .Where(r => r.AllowedStages.Contains(stage))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Recipe>> GetByStageAndSurgeryTypeAsync(
        BariatricStage stage,
        SurgeryType surgeryType,
        CancellationToken cancellationToken = default)
    {
        var stageKey = stage.ToString();
        return await _context.Recipes
            .WithPartitionKey(stageKey)
            .Where(r => r.AllowedStages.Contains(stage)
                        && (!r.CompatibleSurgeryTypes.Any() || r.CompatibleSurgeryTypes.Contains(surgeryType)))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Recipe>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Recipes.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default)
    {
        await _context.Recipes.AddAsync(recipe, cancellationToken);
    }

    public Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken = default)
    {
        recipe.UpdatedAt = DateTime.UtcNow;
        _context.Recipes.Update(recipe);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(string id, string stage, CancellationToken cancellationToken = default)
    {
        var recipe = await GetByIdAsync(id, stage, cancellationToken);
        if (recipe is not null)
            _context.Recipes.Remove(recipe);
    }
}
