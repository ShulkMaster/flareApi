using AutoWrapper;
using FlareApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FlareApi.Config
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new Settings();
            var serilog = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo
                .File("logs/flare.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Configuration.GetSection(nameof(Settings)).Bind(settings);
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog(serilog);
            services.AddCors(c =>
            {
                c.AddDefaultPolicy(o => o.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });
            services.AddMiddleware(settings);
            services.AddServiceLayer();
            services.AddSwag();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllers();
            services.AddDbContext<FlareContext>(options =>
            {
                options.UseMySQL(settings.ConnectionString, config => config.CommandTimeout(120))
                    .UseLoggerFactory(loggerFactory);
                if (_env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging().EnableDetailedErrors();
                }
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlareApi v1"));
                app.UseCors();
            }

            app.UseHttpsRedirection();
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                IsDebug = env.IsDevelopment(),
                UseCustomSchema = true,
                EnableResponseLogging = env.IsDevelopment(),
                IgnoreNullValue = false,
            });
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}