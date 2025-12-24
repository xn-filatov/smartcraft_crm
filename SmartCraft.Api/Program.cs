using Microsoft.EntityFrameworkCore;
using SmartCraft.Api.Middlewares;
using SmartCraft.Infrastructure.Data;
using System.Text.Json.Serialization;
using SmartCraft.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.AddSwagger();
builder.AddDependencyInjection();
builder.AddDatabase();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.AddKeycloak();

var app = builder.Build();

app.UseMiddleware<RequestLoggerMiddleware>();

// Apply CORS globally
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migrate database
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "This is SmartCraft CRM backend");
app.Run();
