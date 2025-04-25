using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Load JWT configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings.GetValue<string>("Key") ?? throw new InvalidOperationException("JWT Key not found.");
var issuer = jwtSettings.GetValue<string>("Issuer") ?? throw new InvalidOperationException("JWT Issuer not found.");
var audience = jwtSettings.GetValue<string>("Audience") ?? throw new InvalidOperationException("JWT Audience not found.");

// 2️⃣ Add services
builder.Services.AddControllers();

// 3️⃣ Configure EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// 4️⃣ Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins(
                "http://localhost:4200",
                "http://localhost:4300",
                "http://192.168.28.44:4200",
                "http://192.168.28.44:4300",
                "http://192.168.28.44:8080" // 👈 Make sure your frontend port is included
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// 5️⃣ Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

var app = builder.Build();

// 6️⃣ Use CORS policy
app.UseCors("AllowFrontend");

// 7️⃣ Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
