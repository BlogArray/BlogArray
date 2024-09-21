﻿using Asp.Versioning;
using BlogArray.Application.Features.Users.Commands;
using BlogArray.Infrastructure.Repositories;
using BlogArray.Persistence;
using BlogArray.Persistence.Sqlite;
using BlogArray.Persistence.SqlServer;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddProblemDetails();

            services.AddLowercaseUrlsRouting();

            services.AddVersioning();

            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            services.ConfigureSwagger();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(AuthenticateCommand))));

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

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            }).AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(s =>
            {
                //s.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogArray API", Version = "v1" });

                s.OperationFilter<SwaggerDefaultValues>();

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Put **_ONLY_** your JWT Bearer token on textbox below!",
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });

                OpenApiSecurityScheme? key = new()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    },
                    In = ParameterLocation.Header
                };

                OpenApiSecurityRequirement? requirement = new() { { key, new List<string>() } };

                s.AddSecurityRequirement(requirement);

                string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            Assembly[] assembliesToScan =
            [
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(AccountRepository))
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
