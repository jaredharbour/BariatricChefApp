using BariatricChefApp.Core.Domain;
using BariatricChefApp.Core.Domain.Enums;

namespace BariatricChefApp.Core.Services;

/// <summary>
/// Validates and describes nutritional requirements per bariatric stage.
/// Guidelines sourced from standard post-bariatric dietary protocols.
/// </summary>
public class NutritionService
{
    // Minimum protein grams per serving by stage
    private static readonly Dictionary<BariatricStage, double> MinProteinByStage = new()
    {
        { BariatricStage.PreOp,               10 },
        { BariatricStage.Stage1_ClearLiquid,   5 },
        { BariatricStage.Stage2_FullLiquid,   10 },
        { BariatricStage.Stage3_Pureed,       15 },
        { BariatricStage.Stage4_SoftFood,     20 },
        { BariatricStage.Stage5_Regular,      20 },
    };

    // Maximum sugar grams per serving by stage (dumping syndrome risk)
    private static readonly Dictionary<BariatricStage, double> MaxSugarByStage = new()
    {
        { BariatricStage.PreOp,               10 },
        { BariatricStage.Stage1_ClearLiquid,   5 },
        { BariatricStage.Stage2_FullLiquid,    5 },
        { BariatricStage.Stage3_Pureed,        8 },
        { BariatricStage.Stage4_SoftFood,     10 },
        { BariatricStage.Stage5_Regular,      15 },
    };

    // Maximum serving size in oz by stage
    private static readonly Dictionary<BariatricStage, double> MaxServingSizeOzByStage = new()
    {
        { BariatricStage.PreOp,               8.0 },
        { BariatricStage.Stage1_ClearLiquid,  4.0 },
        { BariatricStage.Stage2_FullLiquid,   4.0 },
        { BariatricStage.Stage3_Pureed,       4.0 },
        { BariatricStage.Stage4_SoftFood,     6.0 },
        { BariatricStage.Stage5_Regular,      8.0 },
    };

    public NutritionValidationResult Validate(Recipe recipe, BariatricStage stage)
    {
        var issues = new List<string>();
        var nutrition = recipe.NutritionInfo;

        if (MinProteinByStage.TryGetValue(stage, out var minProtein) && nutrition.ProteinGrams < minProtein)
            issues.Add($"Protein ({nutrition.ProteinGrams}g) is below the minimum {minProtein}g for {stage}.");

        if (MaxSugarByStage.TryGetValue(stage, out var maxSugar) && nutrition.SugarGrams > maxSugar)
            issues.Add($"Sugar ({nutrition.SugarGrams}g) exceeds the maximum {maxSugar}g for {stage} — dumping syndrome risk.");

        if (MaxServingSizeOzByStage.TryGetValue(stage, out var maxOz) && recipe.ServingSizeOz > maxOz)
            issues.Add($"Serving size ({recipe.ServingSizeOz}oz) exceeds the recommended {maxOz}oz for {stage}.");

        return new NutritionValidationResult(issues.Count == 0, issues);
    }

    public StageNutritionGuideline GetGuidelinesForStage(BariatricStage stage)
    {
        return new StageNutritionGuideline(
            Stage: stage,
            MinProteinGrams: MinProteinByStage.GetValueOrDefault(stage),
            MaxSugarGrams: MaxSugarByStage.GetValueOrDefault(stage),
            MaxServingSizeOz: MaxServingSizeOzByStage.GetValueOrDefault(stage)
        );
    }
}

public record NutritionValidationResult(bool IsValid, IReadOnlyList<string> Issues);

public record StageNutritionGuideline(
    BariatricStage Stage,
    double MinProteinGrams,
    double MaxSugarGrams,
    double MaxServingSizeOz);
