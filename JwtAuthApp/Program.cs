using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JwtAuthApp.Data;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Database Configuration (ApplicationDbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ JWT Authentication Configuration
var jwtSection = builder.Configuration.GetSection("Jwt");
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
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!)),
            ClockSkew = TimeSpan.Zero // To make the token expire exactly at the defined expiration time
        };
    });

// 3️⃣ CORS Configuration (AllowFrontend Policy)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins(
                "http://localhost:4200",  // Add your frontend URLs here
                "http://localhost:4300",
                "http://192.168.28.44:4200",
                "http://192.168.28.44:4300",
                "http://192.168.28.44:8080"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// 4️⃣ MVC + Swagger Configuration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5️⃣ Development tools (Swagger UI)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 6️⃣ Middleware Setup
app.UseStaticFiles();
app.UseRouting();

// ⚠️ Handle OPTIONS requests and add CORS headers manually
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", context.Request.Headers["Origin"]);
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");

    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        return;
    }

    await next();
});

// Apply CORS policy
app.UseCors("AllowFrontend");

// Authentication and Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// Route to Controllers
app.MapControllers();

// Optional fallback for Angular (if serving from backend)
app.MapFallbackToFile("index.html");

// Run the app
app.Run();
