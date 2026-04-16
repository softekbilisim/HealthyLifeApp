using HealthyLifeApp.Modules.Identity;
using HealthyLifeApp.Modules.Smoking;
using HealthyLifeApp.Modules.Nutrition;
using HealthyLifeApp.Modules.Sugar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Modules
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddSmokingModule(builder.Configuration);
builder.Services.AddNutritionModule(builder.Configuration);
builder.Services.AddSugarModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map Module Endpoints
app.MapIdentityEndpoints();
app.MapSmokingEndpoints();
app.MapNutritionEndpoints();
app.MapSugarEndpoints();

app.Run();
