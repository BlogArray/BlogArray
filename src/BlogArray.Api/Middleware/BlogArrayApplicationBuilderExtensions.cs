using Asp.Versioning.ApiExplorer;
using BlogArray.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Api.Middleware;

/// <summary>
/// Contains extension methods for configuring the BlogArray application.
/// </summary>
public static class BlogArrayApplicationBuilderExtensions
{
    /// <summary>
    /// Configures middleware for the BlogArray application.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance with the configured middleware.</returns>
    public static IApplicationBuilder UseBlogArray(this IApplicationBuilder app)
    {
        // Exception handler middleware
        app.UseExceptionHandler();

        // Adds Swagger and API versioning
        app.AddSwagger();

        // Redirect HTTP requests to HTTPS
        app.UseHttpsRedirection();

        // Configures request routing
        app.UseRouting();

        // Serves static files (e.g., images, CSS)
        app.UseStaticFiles();

        // Enables authentication middleware
        app.UseAuthentication();

        // Enables authorization middleware
        app.UseAuthorization();

        return app;
    }

    /// <summary>
    /// Adds Swagger and API versioning UI configuration to the application.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance with Swagger configuration.</returns>
    private static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
    {
        // Enable Swagger middleware
        app.UseSwagger();

        // Resolve the API version description provider
        IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        // Configure Swagger UI to support multiple API versions
        app.UseSwaggerUI(options =>
        {
            foreach (ApiVersionDescription? description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }

    /// <summary>
    /// Automatically applies pending migrations to the database.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance after applying migrations.</returns>
    public static IApplicationBuilder AddBlogArrayMigration(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Apply pending migrations if any exist
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }

        return app;
    }
}
