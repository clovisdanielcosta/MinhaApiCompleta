using Elmah.Io.Extensions.Logging;

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

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
 