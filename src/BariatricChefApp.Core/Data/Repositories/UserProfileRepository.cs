using BariatricChefApp.Core.Domain;
using BariatricChefApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BariatricChefApp.Core.Data.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly BariatricChefDbContext _context;

    public UserProfileRepository(BariatricChefDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfile?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserProfiles
            .WithPartitionKey(userId)
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        await _context.UserProfiles.AddAsync(profile, cancellationToken);
    }

    public Task UpdateAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        profile.UpdatedAt = DateTime.UtcNow;
        _context.UserProfiles.Update(profile);
        return Task.CompletedTask;
    }
}
