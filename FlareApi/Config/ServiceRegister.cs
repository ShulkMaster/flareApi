using System.Linq;
using System.Text.Json.Serialization;
using FlareApi.Api;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace FlareApi.Config
{
    public static class ServiceRegister
    {
        public static void AddServiceLayer(this IServiceCollection services, Settings settings)
        {
            services.AddSingleton(settings);
            services.AddControllers()
                .AddFluentValidation(validation => validation.RegisterValidatorsFromAssembly(typeof(Startup).Assembly))
                .AddJsonOptions(options =>
                {
                    //options.JsonSerializerOptions.Converters.Add(new JsonDateConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState.Values
                            .Where(m => m.Errors.Count > 0)
                            .SelectMany(m => m.Errors);
                        var list = errors.Select(error =>
                        {
                            var innerError = error.Exception;
                            ApiError? cause = null;
                            if (innerError is not null)
                            {
                                cause = new ApiError(innerError.Source ?? "error", innerError.Message);
                            }

                            return new ApiError("Error", error.ErrorMessage, cause);
                        });
                        return new BadRequestObjectResult(list);
                    };
                });
            //services.AddScoped<ISessionService, SessionService>();
            //services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(Startup));
        }
    }
}