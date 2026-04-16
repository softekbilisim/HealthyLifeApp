using Microsoft.AspNetCore.Identity;

namespace HealthyLifeApp.Modules.Identity.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public string? BloodType { get; set; }
    public double? Weight { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
}
