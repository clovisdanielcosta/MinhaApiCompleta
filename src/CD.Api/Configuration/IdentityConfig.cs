using CD.Api.Data;
using CD.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CD.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, 
                                                                IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();

            //Adicionando configurações do JWT

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSetttings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSetttings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            return services;
        }
    }
}
