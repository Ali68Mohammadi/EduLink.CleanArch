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

    //config our Services from WebApplicationBuilder Hier
    builder.AddPresentaion();

    //config our Interfaces(repository) and  DbContext Hier
    builder.Services.AddInfrastructure(builder.Configuration);

    //config our Services from IServiceCollection Hier
    builder.Services.AddApplication();



    //**//////////////////////////////////////////////////////////////////////////////////////////////////**//

    var app = builder.Build();


    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IEdulinkSeeder>();

    await seeder.SeedAsync();

    app.UseMiddleware<GlobalErrorHandlerMiddleware>();
    //app.UseMiddleware<RequestTimeLoggingMiddleware>();


    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
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

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Startup faield");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
