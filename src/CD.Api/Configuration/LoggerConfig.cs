﻿using CD.Api.Extensions;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CD.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "7f4135736d9a430285069c95d127b942";
                o.LogId = new Guid("1dff6ca0-c84d-41fe-b1c3-be9104d17237");
            });

            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "7f4135736d9a430285069c95d127b942";
            //        o.LogId = new Guid("1dff6ca0-c84d-41fe-b1c3-be9104d17237");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            services.AddHealthChecks()
                .AddCheck("Produtos", new SqlServerHealthChecks(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/api/hc-ui";
                options.ResourcesPath = "/api/hc-ui-resources";

                options.UseRelativeApiPath = false;
                options.UseRelativeResourcesPath = false;
                options.UseRelativeWebhookPath = false;
            });

            return app;
        }
    }
}
 