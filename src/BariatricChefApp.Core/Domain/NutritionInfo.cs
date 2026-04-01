namespace BariatricChefApp.Core.Domain;

/// <summary>
/// Nutritional information for a recipe or ingredient.
/// All values are per serving.
/// </summary>
public class NutritionInfo
{
    public double Calories { get; set; }
    public double ProteinGrams { get; set; }
    public double FatGrams { get; set; }
    public double SugarGrams { get; set; }
    public double CarbGrams { get; set; }
    public double FiberGrams { get; set; }
}
