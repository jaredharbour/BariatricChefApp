using BariatricChefApp.Core.Domain.Enums;

namespace BariatricChefApp.Core.Domain;

public class Recipe
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;

    /// <summary>Serving size in fluid ounces (important for post-op portion control).</summary>
    public double ServingSizeOz { get; set; }

    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }

    /// <summary>Bariatric stages this recipe is appropriate for.</summary>
    public List<BariatricStage> AllowedStages { get; set; } = [];

    /// <summary>Surgery types this recipe is suitable for. Empty means all types.</summary>
    public List<SurgeryType> CompatibleSurgeryTypes { get; set; } = [];

    public NutritionInfo NutritionInfo { get; set; } = new();
    public List<Ingredient> Ingredients { get; set; } = [];
    public List<string> Tags { get; set; } = [];

    /// <summary>Partition key for CosmosDB — set to the primary allowed stage string.</summary>
    public string Stage { get; set; } = string.Empty;

    public string CreatedByUserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
