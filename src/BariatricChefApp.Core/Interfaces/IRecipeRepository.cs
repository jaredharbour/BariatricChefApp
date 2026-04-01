using BariatricChefApp.Core.Domain;
using BariatricChefApp.Core.Domain.Enums;

namespace BariatricChefApp.Core.Interfaces;

public interface IRecipeRepository
{
    Task<Recipe?> GetByIdAsync(string id, string stage, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Recipe>> GetByStageAsync(BariatricStage stage, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Recipe>> GetByStageAndSurgeryTypeAsync(BariatricStage stage, SurgeryType surgeryType, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default);
    Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, string stage, CancellationToken cancellationToken = default);
}
