using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Configurations;
using YGKAPI.Application.Abstractions.Services.Email;
using YGKAPI.Application.Abstractions.Services.ExternalLogins;
using YGKAPI.Application.Abstractions.Token;
using YGKAPI.Application.Storage;
using YGKAPI.Infrastructure.Enums;
using YGKAPI.Infrastructure.Services.Configurator;
using YGKAPI.Infrastructure.Services.Email;
using YGKAPI.Infrastructure.Services.ExternalLogins;
using YGKAPI.Infrastructure.Services.Storage.AWSS3;
using YGKAPI.Infrastructure.Services.Storage.Azure;
using YGKAPI.Infrastructure.Services.Storage.Local;

namespace YGKAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenHandler, Services.Token.TokenHandler>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IExternalLoginService, ExternalLoginService>();

            services.AddAuthentication()
                    .AddGoogle(x =>
                    {
                        x.ClientId = configuration["ExternalLogins:Google:ClientID"];
                        x.ClientSecret = configuration["ExternalLogins:Google:ClientSecret"];
                    });
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    serviceCollection.AddScoped<IStorage, AwsStorage>(); // not implemented
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}