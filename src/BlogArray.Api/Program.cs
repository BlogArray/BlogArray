using BlogArray.Api.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlogArray(builder.Environment, builder.Configuration);

WebApplication app = builder.Build();

await app.UseBlogArray();

app.Run();
