using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PageRightsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string not found.")));

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PageRights API", Version = "v1" });
});

// Optional: Use specific port for local testing only (IIS will manage port separately)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5038);
});

var app = builder.Build();

// Global error handler to send JSON on crash
app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            await context.Response.WriteAsync($"{{\"error\": \"{error.Error.Message}\"}}");
        }
    });
});

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PageRights API V1");
    c.RoutePrefix = string.Empty;
});

// Enable CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection(); // Optional if not using HTTPS in IIS
app.UseAuthorization();

app.MapControllers();

app.Run();
