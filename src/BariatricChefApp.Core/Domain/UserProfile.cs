using BariatricChefApp.Core.Domain.Enums;

namespace BariatricChefApp.Core.Domain;

public class UserProfile
{
    /// <summary>Azure AD B2C object ID — used as partition key.</summary>
    public string UserId { get; set; } = string.Empty;

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DisplayName { get; set; } = string.Empty;
    public SurgeryType SurgeryType { get; set; }
    public BariatricStage CurrentStage { get; set; }
    public DateTime? SurgeryDate { get; set; }
    public List<string> DietaryRestrictions { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
