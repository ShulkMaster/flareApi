using System;
using System.Text.Json.Serialization;
using FlareApi.Api.V1.DataAccess;
using FlareApi.Repository;
using FlareApi.Service;
using FlareApi.Service.Driver;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FlareApi.Config
{
    public static class ServiceRegister
    {
        public static void AddMiddleware(this IServiceCollection services, Settings settings)
        {
            services.AddSingleton(settings);
            services.AddControllers()
                .AddFluentValidation(validation => validation.RegisterValidatorsFromAssembly(typeof(Startup).Assembly))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonDateConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddAuthentication(s =>
                {
                    s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(token =>
                {
                    token.SaveToken = true;
                    token.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(settings.GetEncodeSecret()),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(nameof(FlarePolicy), policy => { policy.RequireRole(FlarePolicy.Roles); });
            });
        }

        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddAutoMapper(typeof(Startup));
        }

        public static void AddSwag(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "FlareApi", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
        
    }
}
