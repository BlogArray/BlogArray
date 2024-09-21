using Asp.Versioning.ApiExplorer;
using BlogArray.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Api.Middleware
{
    public static class BlogArrayApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBlogArray(this IApplicationBuilder app)
        {
            app.UseExceptionHandler();

            app.AddSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }

        private static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwaggerUI(options =>
            {
                foreach (ApiVersionDescription? description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }

        public static IApplicationBuilder AddBlogArrayMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}
