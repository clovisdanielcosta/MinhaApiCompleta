using CD.Business.Interfaces;
using CD.Data.Context;
using CD.Data.Repository;

namespace CD.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            return services;
        }
    }
}
