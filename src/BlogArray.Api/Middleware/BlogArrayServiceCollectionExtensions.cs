﻿using BlogArray.Persistence;
using BlogArray.Persistence.Sqlite;
using BlogArray.Persistence.SqlServer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using System.Reflection;
using System.Text;

namespace BlogArray.Api.Middleware
{
    public static class BlogArrayServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogArray(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddLowercaseUrlsRouting();

            services.ConfigureSwagger();

            services.ConfigureRepositories()
                .AddConnectionProvider(environment, configuration)
                .AddAppAuthentication(configuration);

            return services;
        }
        public static IServiceCollection AddLowercaseUrlsRouting(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            return services;
        }

        private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogArray API", Version = "v1" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }},
                    Array.Empty<string>()
                }});
            });

            return services;
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            Assembly[] assembliesToScan =
            [
                Assembly.GetExecutingAssembly(),
            //Assembly.GetAssembly(typeof(AppDbContext))
            ];

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan).AsPublicImplementedInterfaces();

            return services;
        }

        private static IServiceCollection AddConnectionProvider(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            string? databaseType = configuration.GetValue("BlogArray:DatabaseType", "SqlServer");

            string? connectionString = configuration.GetValue<string>("BlogArray:BlogArrayConnection");

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

    }
}
