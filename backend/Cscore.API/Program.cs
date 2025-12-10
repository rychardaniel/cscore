using System.Text;
using Cscore.API.Data;
using Cscore.API.Middlewares;
using Cscore.API.Repositories;
using Cscore.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<MongoContext>();

// Repositories
builder.Services.AddScoped<IChampionshipRepository, ChampionshipRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChampionshipJudgeRepository, ChampionshipJudgeRepository>();

// MongoDB Repositories
builder.Services.AddScoped<Cscore.API.Data.MongoDB.Repositories.IMatchScoreRepository, Cscore.API.Data.MongoDB.Repositories.MatchScoreRepository>();
builder.Services.AddScoped<Cscore.API.Data.MongoDB.Repositories.IMatchEventRepository, Cscore.API.Data.MongoDB.Repositories.MatchEventRepository>();

// Services
builder.Services.AddScoped<IChampionshipService, ChampionshipService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChampionshipJudgeService, ChampionshipJudgeService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]!);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "cscore-api",
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "cscore-client"
    };
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["jwt"];
            return Task.CompletedTask;
        }
    };
});

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("JudgeOrAdmin", policy =>
        policy.RequireRole("Judge", "Admin"));
});

builder.Services.AddScoped<IAuthorizationHandler, Cscore.API.Authorization.Handlers.JudgeAuthorizationHandler>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UsePathBase("/api");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();