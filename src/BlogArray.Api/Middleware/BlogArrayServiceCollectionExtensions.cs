using Asp.Versioning;
using BlogArray.Application.Authorization;
using BlogArray.Application.Features.Account.Commands;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Caching;
using BlogArray.Infrastructure.Repositories;
using BlogArray.Persistence;
using BlogArray.Persistence.Sqlite;
using BlogArray.Persistence.SqlServer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using System.Reflection;
using System.Text;

namespace BlogArray.Api.Middleware;

/// <summary>
/// Contains extension methods for configuring services in the BlogArray application.
/// </summary>
public static class BlogArrayServiceCollectionExtensions
{
    /// <summary>
    /// Configures the BlogArray services and middleware.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="environment">The IWebHostEnvironment instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>The IServiceCollection instance after configuration.</returns>
    public static IServiceCollection AddBlogArray(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.ConfigureRepositories();
        services.AddVersioning();
        services.ConfigureSwagger();
        services.AddConnectionProvider(environment, configuration);
        services.AddAppAuthentication(configuration);
        services.AddCache(configuration);

        services.AddMediatR(o => o.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(AuthenticateCommand))));

        return services;
    }

    /// <summary>
    /// Configures API versioning with support for URL segments, headers, and media types.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The IServiceCollection instance with versioning configured.</returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version")
            );
        }).AddApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    /// <summary>
    /// Configures Swagger for the BlogArray API with JWT security support.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The IServiceCollection instance with Swagger configured.</returns>
    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            s.OperationFilter<SwaggerDefaultValues>();

            s.SchemaFilter<EnumSchemaFilter>();

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme. Enter only the JWT token.",
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http
            });

            OpenApiSecurityScheme? securityScheme = new()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                In = ParameterLocation.Header
            };

            OpenApiSecurityRequirement securityRequirement = new()
            {
                { securityScheme, new List<string>() }
            };

            s.AddSecurityRequirement(securityRequirement);

            string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            s.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    /// <summary>
    /// Registers repository implementations from specified assemblies.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The IServiceCollection instance with repositories configured.</returns>
    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        Assembly[] assembliesToScan =
        [
            Assembly.GetAssembly(typeof(AccountRepository)),
            Assembly.GetExecutingAssembly(),
        ];

        services.AddSingleton<IAuthorizationHandler, EditPostAuthorizationHandler>();

        services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan).AsPublicImplementedInterfaces();

        services.RegisterAssemblyPublicNonGenericClasses()
                .Where(c => c.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces();

        return services;
    }

    /// <summary>
    /// Adds the database connection provider based on environment configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="environment">The IWebHostEnvironment instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>The IServiceCollection instance with the appropriate database connection configured.</returns>
    private static IServiceCollection AddConnectionProvider(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        string? databaseType = configuration.GetValue("BlogArray:Database:Type", "SqlServer");
        string? connectionString = configuration.GetValue<string>("BlogArray:Database:ConnectionString");

        switch (databaseType)
        {
            case "SqlServer":
                services.AddDbContext<AppDbContext, SqlServerDbContext>(options =>
                    options.UseSqlServer(connectionString, x => x.MigrationsAssembly("BlogArray.Persistence.SqlServer")));
                break;
            case "Sqlite":
                SqliteConnectionStringBuilder connectionStringBuilder = new(connectionString);
                string dataSourcePath = Path.Combine(environment.ContentRootPath, connectionStringBuilder.DataSource);
                string? dataSourceDirectory = Path.GetDirectoryName(dataSourcePath);
                if (!string.IsNullOrEmpty(dataSourceDirectory) && !Directory.Exists(dataSourceDirectory))
                {
                    Directory.CreateDirectory(dataSourceDirectory);
                }

                services.AddDbContext<AppDbContext, SqliteDbContext>(options =>
                    options.UseSqlite(connectionString, x => x.MigrationsAssembly("BlogArray.Persistence.Sqlite")));
                break;
            default:
                throw new Exception("Database type not supported");
        }

        return services;
    }

    /// <summary>
    /// Configures JWT-based authentication for the BlogArray application.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>The IServiceCollection instance with authentication configured.</returns>
    private static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Jwt";
            options.DefaultChallengeScheme = "Jwt";
        }).AddJwtBearer("Jwt", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration.GetValue<string>("BlogArray:Jwt:Audience"),
                ValidIssuer = configuration.GetValue<string>("BlogArray:Jwt:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("BlogArray:Jwt:Key"))),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Query.TryGetValue("token", out StringValues token))
                    {
                        context.Token = token;
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context => Task.CompletedTask
            };
        });

        return services;
    }

    /// <summary>
    /// Configures cache for the BlogArray application.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>The IServiceCollection instance with cache configured.</returns>
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<InMemoryCacheService>();
        services.AddScoped<RedisCacheService>();

        // Read the cache type from configuration
        string? cacheType = configuration.GetValue<string>("BlogArray:Cache:Type", "InMemory");

        // Configure cache options
        services.Configure<CacheConfiguration>(configuration.GetSection("BlogArray:Cache"));

        // Add memory cache service (for in-memory caching)
        services.AddMemoryCache();

        // Add Redis cache service if needed (assumes you’ve registered RedisCacheService elsewhere)
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("BlogArray:Cache:ConnectionString");
        });


        // Register cache services based on the cache type
        services.AddScoped<ICacheService>(serviceProvider =>
        {
            switch (cacheType?.ToLowerInvariant())
            {
                case "redis":
                    return serviceProvider.GetService<RedisCacheService>() ?? throw new InvalidOperationException("RedisCacheService is not available");
                case "inmemory":
                default:
                    return serviceProvider.GetService<InMemoryCacheService>() ?? throw new InvalidOperationException("InMemoryCacheService is not available");
            }
        });
        return services;
    }

}
