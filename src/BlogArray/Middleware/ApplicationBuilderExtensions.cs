using BlogArray.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Middleware;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddMigration(this IApplicationBuilder app, IConfiguration configuration)
    {
        DbContextOptionsBuilder<AppDbContext> options = new();
        options.UseSqlServer(configuration.GetConnectionString("Default"));

        using (AppDbContext context = new(options.Options))
        {
            context.Database.Migrate();
        }
        return app;
    }
}
