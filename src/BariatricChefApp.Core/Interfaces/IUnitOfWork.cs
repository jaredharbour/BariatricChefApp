namespace BariatricChefApp.Core.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IRecipeRepository Recipes { get; }
    IUserProfileRepository UserProfiles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
