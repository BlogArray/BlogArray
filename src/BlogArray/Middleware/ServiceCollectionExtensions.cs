using BlogArray.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace BlogArray.Middleware;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        Assembly[] assembliesToScan =
        [
            Assembly.GetExecutingAssembly(),
            Assembly.GetAssembly(typeof(AppDbContext))
        ];

        services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan).AsPublicImplementedInterfaces();

        return services;
    }

    public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseType = configuration.GetValue("DatabaseType", "SqlServer");

        switch (databaseType)
        {
            case "SqlServer":
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));
                break;
            case "Sqlite":
                services.AddDbContext<AppDbContext>(options => options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));
                break;
            default:
                throw new Exception("Database type not supported");
        }

        //services.AddIdentity<AppUser, IdentityRole<int>>(opt =>
        //{
        //    opt.Lockout.AllowedForNewUsers = false;
        //    opt.SignIn.RequireConfirmedEmail = true;
        //    opt.User.RequireUniqueEmail = true;
        //    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
        //}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddLowercaseUrlsRouting(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }
}
