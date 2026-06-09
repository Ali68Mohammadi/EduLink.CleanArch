using EduLink.Api.Extensions;
using EduLink.Api.Middlewares;
using EduLink.Application.Extensions;
using EduLink.Domain.Entities;
using EduLink.Infrastructure.Extensions;
using EduLink.Infrastructure.Seeder;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // ================================
    // 🔥 SERILOG SETUP (IMPORTANT)
    // ================================
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    builder.Host.UseSerilog();

    // ================================
    // 🔥 IMPORTANT FOR AZURE LOG STREAM
    // ================================
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    // ================================
    // SERVICES
    // ================================
    builder.AddPresentaion();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();


    var app = builder.Build();

    // Seeder (optional)
    // var scope = app.Services.CreateScope();
    // var seeder = scope.ServiceProvider.GetRequiredService<IEdulinkSeeder>();
    // await seeder.SeedAsync();

    // Middleware pipeline
    app.UseMiddleware<GlobalErrorHandlerMiddleware>();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    Log.Information("🔥 Application Started Successfully");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Startup failed");
    Console.WriteLine(ex.ToString());
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }