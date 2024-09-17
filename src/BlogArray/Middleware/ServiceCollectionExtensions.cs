using BlogArray.Persistence;
using BlogArray.Persistence.Sqlite;
using BlogArray.Persistence.SqlServer;
using Microsoft.Data.Sqlite;
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
            //Assembly.GetAssembly(typeof(AppDbContext))
        ];

        services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan).AsPublicImplementedInterfaces();

        return services;
    }

    public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        string? databaseType = configuration.GetValue("DatabaseType", "SqlServer");

        string? connectionString = configuration.GetConnectionString("BlogArrayConnection");

        switch (databaseType)
        {
            case "SqlServer":
                services.AddDbContext<AppDbContext, SqlServerDbContext>(options =>
                options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("BlogArray.Persistence.SqlServer")));
                break;
            case "Sqlite":
                var sonnectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);
                var dataSourcePath = Path.Combine(environment.ContentRootPath, sonnectionStringBuilder.DataSource);
                var dataSourceDirectory = Path.GetDirectoryName(dataSourcePath);
                if (!string.IsNullOrEmpty(dataSourceDirectory) && !Directory.Exists(dataSourceDirectory)) Directory.CreateDirectory(dataSourceDirectory);

                services.AddDbContext<AppDbContext, SqliteDbContext>(options =>
                options.UseSqlite(connectionString,
                x => x.MigrationsAssembly("BlogArray.Persistence.Sqlite")));
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
