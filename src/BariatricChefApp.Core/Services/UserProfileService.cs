using BariatricChefApp.Core.Domain;
using BariatricChefApp.Core.Interfaces;

namespace BariatricChefApp.Core.Services;

public class UserProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserProfile?> GetProfileAsync(string userId, CancellationToken cancellationToken = default)
        => await _unitOfWork.UserProfiles.GetByUserIdAsync(userId, cancellationToken);

    public async Task<UserProfile> CreateOrUpdateProfileAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.UserProfiles.GetByUserIdAsync(profile.UserId, cancellationToken);

        if (existing is null)
        {
            profile.CreatedAt = DateTime.UtcNow;
            profile.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.UserProfiles.AddAsync(profile, cancellationToken);
        }
        else
        {
            existing.DisplayName = profile.DisplayName;
            existing.SurgeryType = profile.SurgeryType;
            existing.CurrentStage = profile.CurrentStage;
            existing.SurgeryDate = profile.SurgeryDate;
            existing.DietaryRestrictions = profile.DietaryRestrictions;
            await _unitOfWork.UserProfiles.UpdateAsync(existing, cancellationToken);
            profile = existing;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return profile;
    }
}
