using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Domain.Entities.Identity;
using YGKAPI.Persistence.Contexts.MySQL;
using YGKAPI.Persistence.Repositories.Auth;
using YGKAPI.Persistence.Services.Identity;
using Microsoft.AspNetCore.Identity;
using YGKAPI.Application.Abstractions.Services.Email;
using YGKAPI.Application.Abstractions.Services.Endpoint;
using YGKAPI.Persistence.Services.Endpoint;
using YGKAPI.Application.Abstractions.Services.ClientEndpoint;
using YGKAPI.Persistence.Services.ClientEndpoint;
using YGKAPI.Application.Repositories.DbCredentials;
using YGKAPI.Persistence.Repositories.DbCredentials;
using YGKAPI.Application.Abstractions.Services.DbServices;
using YGKAPI.Persistence.Services.DbServices;

namespace YGKAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:MySQL"];
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            services.AddDbContext<YGKAPIMySQLDbContext>(options => options.UseMySql(connectionString, serverVersion));
            services.AddIdentity<AppUser, AppRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<YGKAPIMySQLDbContext>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEndpointService, EndpointService>();
            services.AddScoped<IClientEndpointService, ClientEndpointService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMySqlService, MySqlService>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IEndpointRepository, EndpointRepository>();
            services.AddScoped<IClientEndpointRepository, ClientEndpointRepository>();
            services.AddScoped<IClientEndpointMenuRepository, ClientEndpointMenuRepository>();
            services.AddScoped<IAPIClientRepository, APIClientRepository>();
            services.AddScoped<IDbCredentialsRepository, DbCredentialsRepository>();
        }
    }
}