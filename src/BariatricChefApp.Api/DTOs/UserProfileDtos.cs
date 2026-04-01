using BariatricChefApp.Core.Domain.Enums;

namespace BariatricChefApp.Api.DTOs;

public record UserProfileDto(
    string UserId,
    string DisplayName,
    SurgeryType SurgeryType,
    BariatricStage CurrentStage,
    DateTime? SurgeryDate,
    List<string> DietaryRestrictions);

public record UserProfileUpdateRequest(
    string DisplayName,
    SurgeryType SurgeryType,
    BariatricStage CurrentStage,
    DateTime? SurgeryDate,
    List<string> DietaryRestrictions);
