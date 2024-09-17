using BlogArray.Domain.Entities;
using BlogArray.Persistence;
using BlogArray.Persistence.Sqlite;
using BlogArray.Persistence.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using NetCore.AutoRegisterDi;
using System.Reflection;
using System.Text;

namespace BlogArray.Middleware;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        Assembly[] assembliesToScan =
        [
            Assembly.GetExecutingAssembly(),
            //Assembly.GetAssembly(typeof(AppDbContext))
        ];

        services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan).AsPublicImplementedInterfaces();

        return services;
    }

    public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        string? databaseType = configuration.GetValue("AppSettings:DatabaseType", "SqlServer");

        string? connectionString = configuration.GetValue<string>("AppSettings:BlogArrayConnection");

        switch (databaseType)
        {
            case "SqlServer":
                services.AddDbContext<AppDbContext, SqlServerDbContext>(options =>
                options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("BlogArray.Persistence.SqlServer")));
                break;
            case "Sqlite":
                SqliteConnectionStringBuilder sonnectionStringBuilder = new(connectionString);
                string dataSourcePath = Path.Combine(environment.ContentRootPath, sonnectionStringBuilder.DataSource);
                string? dataSourceDirectory = Path.GetDirectoryName(dataSourcePath);
                if (!string.IsNullOrEmpty(dataSourceDirectory) && !Directory.Exists(dataSourceDirectory)) Directory.CreateDirectory(dataSourceDirectory);

                services.AddDbContext<AppDbContext, SqliteDbContext>(options =>
                options.UseSqlite(connectionString,
                x => x.MigrationsAssembly("BlogArray.Persistence.Sqlite")));
                break;
            default:
                throw new Exception("Database type not supported");
        }

        return services;
    }

    public static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, IdentityRole<int>>(opt =>
        {
            opt.Lockout.AllowedForNewUsers = false;
            opt.SignIn.RequireConfirmedEmail = true;
            opt.SignIn.RequireConfirmedAccount = true;
            opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
            opt.User.RequireUniqueEmail = true;
            opt.Lockout.MaxFailedAccessAttempts = 3;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
            opt.Password.RequiredLength = 8;
        })/*.AddSignInManager<SignInManagerExtension<AppUser>>()*/
        .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

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
                ValidAudience = configuration.GetValue<string>("AppSettings:Jwt:Audience"),
                ValidIssuer = configuration.GetValue<string>("AppSettings:Jwt:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Jwt:Key"))),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Query.TryGetValue("token", out StringValues token))
                    {
                        context.Token = token;
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    public static IServiceCollection AddLowercaseUrlsRouting(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }
}
