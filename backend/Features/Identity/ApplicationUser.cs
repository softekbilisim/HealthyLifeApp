using Microsoft.AspNetCore.Identity;

namespace HealthyLifeApp.API.Features.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public string? BloodType { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
}
