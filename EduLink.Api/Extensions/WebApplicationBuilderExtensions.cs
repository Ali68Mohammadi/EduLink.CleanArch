using EduLink.Api.Middlewares;
using EduLink.Application.Extensions;
using EduLink.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

namespace EduLink.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// add services from program.cs
    /// </summary>
    /// <param name="builder"></param>
    public static void AddPresentaion(this WebApplicationBuilder builder)
    {
        // Add services to the container.


        builder.Services.AddAuthentication();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"

            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference {Type= ReferenceType.SecurityScheme, Id="bearerAuth"}
        },
        []
      }
    });
        });
        builder.Services.AddEndpointsApiExplorer();


        builder.Services.AddScoped<GlobalErrorHandlerMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

        builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

    }
}
