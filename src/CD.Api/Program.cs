using CD.Api.Configuration;
using CD.Api.Extensions;
using CD.Data.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

//Configure Services

builder.Services.AddDbContext<MeuDbContext>(options =>
{ 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddWebApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.ResolveDependencies();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLoggingConfig(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddCheck("Produtos", new SqlServerHealthChecks(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

builder.Services.AddHealthChecksUI()
    .AddSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));


// Configure app

var app = builder.Build();
var apiDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseAuthentication();

app.UseWebApiConfig(app.Environment);

app.UseSwaggerConfig(apiDescriptionProvider);

app.UseLoggingConfiguration();

app.MapHealthChecks("/api/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/api/hc-ui";
    options.ResourcesPath = "/api/hc-ui-resources";

    options.UseRelativeApiPath = false;
    options.UseRelativeResourcesPath = false;
    options.UseRelativeWebhookPath = false;
});



app.Run();
