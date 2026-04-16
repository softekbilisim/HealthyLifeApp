using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HealthyLifeApp.Modules.Identity.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HealthyLifeApp.Modules.Identity;

public static class IdentityEndpoints
{
    public static void MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/identity");

        group.MapPost("/register", async (RegisterRequest request, UserManager<ApplicationUser> userManager) =>
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Results.BadRequest(result.Errors);

            return Results.Ok(new { Message = "User registered successfully" });
        });

        group.MapPost("/login", async (LoginRequest request, UserManager<ApplicationUser> userManager, IConfiguration configuration) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
                return Results.Unauthorized();

            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? "super_secret_key_for_healthy_life_app_123456";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("FullName", user.FullName ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return Results.Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        });
    }
}

public record RegisterRequest(string Email, string Password, string FullName);
public record LoginRequest(string Email, string Password);
