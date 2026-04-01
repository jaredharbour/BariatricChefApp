using BariatricChefApp.Core.Data.Repositories;
using BariatricChefApp.Core.Interfaces;

namespace BariatricChefApp.Core.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly BariatricChefDbContext _context;
    private IRecipeRepository? _recipes;
    private IUserProfileRepository? _userProfiles;

    public UnitOfWork(BariatricChefDbContext context)
    {
        _context = context;
    }

    public IRecipeRepository Recipes =>
        _recipes ??= new RecipeRepository(_context);

    public IUserProfileRepository UserProfiles =>
        _userProfiles ??= new UserProfileRepository(_context);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
