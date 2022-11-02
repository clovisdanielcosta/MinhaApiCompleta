using CD.Api.Extensions;

namespace CD.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var apiKey = "7f4135736d9a430285069c95d127b942";
            var logId = new Guid("1dff6ca0-c84d-41fe-b1c3-be9104d17237");
            var heartbeatId = "a029e12aad23425e893d6ffebdcf3c51";

            services.AddElmahIo(o =>
            {
                o.ApiKey = apiKey;
                o.LogId = logId;
            });

            services
                .AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = apiKey;
                    options.LogId = logId;
                    options.HeartbeatId = heartbeatId;
                })

                //.AddElmahIoPublisher(options =>
                //{
                //    options.ApiKey = "7f4135736d9a430285069c95d127b942";
                //    options.LogId = new Guid("1dff6ca0-c84d-41fe-b1c3-be9104d17237");
                //    options.HeartbeatId = "API Fornecedores";
                //})
                .AddCheck("Produtos", new SqlServerHealthChecks(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
 