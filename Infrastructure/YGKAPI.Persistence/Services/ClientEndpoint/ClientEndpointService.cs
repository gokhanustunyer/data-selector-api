using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ClientEndpoint;
using YGKAPI.Application.DTOs.ClientEndpoint;
using YGKAPI.Application.Exceptions.User.Auth;
using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Domain.Entities.Auth;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Persistence.Services.ClientEndpoint
{
    public class ClientEndpointService : IClientEndpointService
    {
        private readonly IClientEndpointRepository _clientEndpointRepository;
        private readonly IClientEndpointMenuRepository _clientEndpointMenuRepository;
        private readonly IAPIClientRepository _apiClientRepository;
        private readonly RoleManager<AppRole> _roleManager;
        public ClientEndpointService(IClientEndpointMenuRepository clientEndpointMenuRepository,
                                     IClientEndpointRepository clientEndpointRepository,
                                     RoleManager<AppRole> roleManager,
                                     IAPIClientRepository apiClientRepository)
        {
            _clientEndpointMenuRepository = clientEndpointMenuRepository;
            _clientEndpointRepository = clientEndpointRepository;
            _roleManager = roleManager;
            _apiClientRepository = apiClientRepository;
        }

        public async Task<bool> AssignRoleToEndpointsByRoleId(string roleId, string[] endpointIds)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                foreach (string endpointId in endpointIds)
                {
                    var endpoint = await _clientEndpointRepository.GetByIdAsync(endpointId);
                    if (endpoint != null)
                    {
                        endpoint.Roles.Add(role);
                    }
                    else
                        throw new Exception("clientEndpoint.notFoundException.message");
                }
                await _clientEndpointRepository.SaveAsync();
                return true;
            }
            throw new NotFoundUserException("user.auth.notFoundUserException.message");
        }

        public async Task<bool> CreateAPIClient(string domain, int? port, string description, ClientHttpScheme scheme = ClientHttpScheme.HTTPS)
        {
            var result = await _apiClientRepository.AddAsync(new()
            {
                Port = port,
                Domain = domain,
                Scheme = scheme,
                Description = description
            });
            await _apiClientRepository.SaveAsync();
            return result;
        }

        public async Task<bool> CreateClientEndpoint(string path, string menuId)
        {
            var clientMenu = await _clientEndpointMenuRepository.GetByIdAsync(menuId);
            var result = await _clientEndpointRepository.AddAsync(new()
            {
                Path = path,
                ClientEndpointMenu = clientMenu,
            });
            await _clientEndpointRepository.SaveAsync();
            return result;
        }

        public async Task<bool> CreateClientEndpointMenu(string name, string apiClientId)
        {
            var apiClient = await _apiClientRepository.GetByIdAsync(apiClientId);
            var result = await _clientEndpointMenuRepository.AddAsync(new()
            {
                Name = name,
                APIClient = apiClient
            });
            await _clientEndpointMenuRepository.SaveAsync();
            return result;
        }

        public async Task<List<ClientEndpointMenuList>> GetClientEndpointMenusByRoleId(string roleId, string apiClientId)
        {
            var apiClient = await _apiClientRepository.GetByIdAsync(apiClientId);
            var clientEndpointMenus = _clientEndpointMenuRepository.GetWhere(a => a.APIClient == apiClient, include: a => a.Include(ac => ac.ClientEndpoints)).Select(a => new ClientEndpointMenuList()
            {
                Endpoints = a.ClientEndpoints.Select(ac => ac.Path).ToList(),
                MenuName = a.Name,
            }).ToList();
            return clientEndpointMenus;
        }
    }
}