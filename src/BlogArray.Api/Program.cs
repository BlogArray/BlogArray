using BlogArray.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlogArray(builder.Environment, builder.Configuration);

var app = builder.Build();

app.UseBlogArray();

app.Run();
