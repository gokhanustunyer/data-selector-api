using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Configurations;
using YGKAPI.Application.Abstractions.Services.Endpoint;
using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Domain.Entities.Auth;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Persistence.Services.Endpoint
{
    public class EndpointService : IEndpointService
    {
        private readonly IApplicationService _applicationService;
        private readonly IEndpointRepository _endpointRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly RoleManager<AppRole> _roleManager;
        public EndpointService(IApplicationService applicationService,
                                   IEndpointRepository endpointRepository,
                                   IMenuRepository menuRepository,
                                   RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointRepository = endpointRepository;
            _menuRepository = menuRepository;
            _roleManager = roleManager;
        }

        public async Task AssingRoleEndpoint(string[] roles, string menu, string code, Type type)
        {
            List<AppRole> appRoles = new();
            foreach (string role in roles)
            {
                AppRole appRole = await _roleManager.FindByIdAsync(role);
                appRoles.Add(appRole);
            }

            Menu _menu = await _menuRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };
                await _menuRepository.AddAsync(_menu);
                await _menuRepository.SaveAsync();
            }
            Domain.Entities.Auth.Endpoint? endpoint = await _endpointRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndPoints(type)
                                    .FirstOrDefault(m => m.Name == menu)?
                                    .Actions.FirstOrDefault(e => e.Code == code);
                endpoint = new()
                {
                    Code = action.Code,
                    HttpType = action.HttpType,
                    ActionType = action.ActionType,
                    Definition = action.Definition,
                    Menu = _menu,
                };
                await _endpointRepository.AddAsync(endpoint);
                await _endpointRepository.SaveAsync();
            }
            else
            {
                foreach (var role in endpoint.Roles)
                {
                    endpoint.Roles.Remove(role);
                }
            }
            endpoint.Roles = appRoles;
            await _endpointRepository.SaveAsync();
        }

        public async Task<List<Domain.Entities.Auth.Endpoint>> GetEndpointsByRoleId(string roleId)
            => _endpointRepository.Table.Include(e => e.Roles).Where(e => e.Roles.FirstOrDefault(ep => ep.Id == roleId) != null).ToList();

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
            Domain.Entities.Auth.Endpoint? endpoint = await _endpointRepository.Table.Include(e => e.Roles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            return (endpoint != null) ? endpoint.Roles.Select(e => e.Name).ToList() : new();
        }
    }
}