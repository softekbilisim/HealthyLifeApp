using HealthyLifeApp.Shared.Models;

namespace HealthyLifeApp.Modules.Sugar.Entities;

public class SugarQuittingProgress : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
}

public class SugarHealthMilestone : BaseEntity
{
    public int DaysAfterQuitting { get; set; }
    public string DescriptionTr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
}

public class HealthyRecipe : BaseEntity
{
    public string TitleTr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string ContentTr { get; set; } = string.Empty;
    public string ContentEn { get; set; } = string.Empty;
    public bool IsForSugarCravings { get; set; }
}
