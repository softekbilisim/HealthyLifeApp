using HealthyLifeApp.API.Common;

namespace HealthyLifeApp.API.Features.Smoking;

public class SmokingHabit : BaseEntity
{
    public Guid UserId { get; set; }
    public int DailyCigaretteCount { get; set; }
    public int MinutesPerCigarette { get; set; }
    public decimal PackPrice { get; set; }
    public int CigarettesInPack { get; set; } = 20;
    public DateTime QuitDate { get; set; }
}

public class HealthImprovementMilestone : BaseEntity
{
    public int MinutesAfterQuitting { get; set; }
    public string DescriptionTr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
}
