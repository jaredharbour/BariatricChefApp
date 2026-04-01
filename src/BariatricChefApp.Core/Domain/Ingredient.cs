namespace BariatricChefApp.Core.Domain;

public class Ingredient
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public NutritionInfo? NutritionInfo { get; set; }
}
