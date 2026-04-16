using HealthyLifeApp.Shared.Models;

namespace HealthyLifeApp.Modules.Nutrition.Entities;

public class NutritionProfile : BaseEntity
{
    public Guid UserId { get; set; }
    public string BloodType { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public double DailyCalorieNeed { get; set; }
}

public class FoodSuggestion : BaseEntity
{
    public string NameTr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string BloodType { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Beneficial, Neutral, Harmful
    public bool IsSnack { get; set; }
}
