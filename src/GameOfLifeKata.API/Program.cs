using Microsoft.OpenApi.Models;
using System.Reflection;
using GameOfLifeAPI.Model;
using GameOfLifePersistance;
using Asp.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
namespace GameOfLifeKata.API
{

    public class Program {
        
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddScoped<GameOfLife>();
            builder.Services.AddScoped<BoardRepository, FileSystemBoardRepository>(_ => new FileSystemBoardRepository(GetPath()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddControllers(options =>
            {
                //options.Conventions.Add(new GroupingByNamespaceConvention());
            });


            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GameOfLife API v1",
                    Description = "An ASP.NET Core Web API for managing GameOfLife items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "GameOfLife API v2",
                    Description = "An ASP.NET Core Web API for managing GameOfLife items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });


                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });


            builder.Services.AddHealthChecks()
                .AddCheck<Healthchecks>("Permissions Enable");
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game Of Life API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Game Of Life API V2");
            });
            app.MapHealthChecks("/healthz", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.MapControllers();

            app.Run();
        }
        public static string GetPath()
        {
            var path = "/app/Saved";
            if (OperatingSystem.IsWindows())
            {
                path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifePersistance\GamesJSON";
            }
            Directory.CreateDirectory(path);
            return path;
        }

    }

}
