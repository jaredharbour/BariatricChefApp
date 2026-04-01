using BariatricChefApp.Api.DTOs;
using BariatricChefApp.Core.Domain;

namespace BariatricChefApp.Api.Mapping;

public static class UserProfileMappingExtensions
{
    public static UserProfileDto ToDto(this UserProfile profile) => new(
        UserId: profile.UserId,
        DisplayName: profile.DisplayName,
        SurgeryType: profile.SurgeryType,
        CurrentStage: profile.CurrentStage,
        SurgeryDate: profile.SurgeryDate,
        DietaryRestrictions: profile.DietaryRestrictions);

    public static UserProfile ToDomain(this UserProfileUpdateRequest request) => new()
    {
        DisplayName = request.DisplayName,
        SurgeryType = request.SurgeryType,
        CurrentStage = request.CurrentStage,
        SurgeryDate = request.SurgeryDate,
        DietaryRestrictions = request.DietaryRestrictions
    };
}
