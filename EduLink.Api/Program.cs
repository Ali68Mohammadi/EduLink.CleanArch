using EduLink.Api.Middlewares;
using EduLink.Application.Extensions;
using EduLink.Infrastructure.Extensions;
using EduLink.Infrastructure.Seeder;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//config our Interfaces(repository) and  DbContext Hier
builder.Services.AddInfrastructure(builder.Configuration);

//config our Services Hier
builder.Services.AddApplication();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GlobalErrorHandlerMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//**//////////////////////////////////////////////////////////////////////////////////////////////////**//

var app = builder.Build();


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IEdulinkSeeder>();

await seeder.SeedAsync();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();


app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
