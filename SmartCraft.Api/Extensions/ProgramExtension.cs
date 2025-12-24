using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartCraft.Application.Services;
using SmartCraft.Application.Services.Interfaces;
using SmartCraft.Infrastructure.Data;
using SmartCraft.Infrastructure.Data.Configuration;

namespace SmartCraft.Api.Extensions;

/// <summary>
/// Extension class for Program.cs
/// </summary>
public static partial class Program
{
    /// <summary>
    /// Adds Swagger page
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/></param>
    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartCraft CRM", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            option.IncludeXmlComments(xmlPath);

            option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Paste JWT here. "
            });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });
    }

    /// <summary>
    /// Adds app's services
    /// </summary>
    /// <param name="builder"></param>
    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICompanyService, CompanyService>();
    }

    /// <summary>
    /// Adds keycloak authorization
    /// </summary>
    /// <param name="builder"></param>
    public static void AddKeycloak(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration.GetSection("Keycloak:Authority").Value;
                options.Audience = builder.Configuration.GetSection("Keycloak:Audience").Value;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidateIssuer = false;
            });
        builder.Services.AddAuthorization();
    }

    /// <summary>
    /// Adds database connection
    /// </summary>
    /// <param name="builder"></param>
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("ServicesDB");

        builder.Services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(
                connectionString,
                npgsqlOptionsAction: o =>
                {
                    o.MigrationsHistoryTable("__MigrationHistory", DataSchemaConstants.DEFAULT_SCHEMA_NAME);
                    o.MigrationsAssembly(typeof(Program).Assembly.FullName);
                })
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
        );
    }
}