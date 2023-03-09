using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Abstractions.Services.Configuration;
using E_Commerce.Application.Exceptions;
using E_Commerce.Application.Features.Queries.AssignRoleEndpoint.GetRolesToEndpoint;
using E_Commerce.Application.Repositories.ElementsRepositories;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Persistence.Repositories.ElementsServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace E_Commerce.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepository _endpoindReadRepository;
        readonly IEndpointWriteRepository _endpoinWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpoinReadRepository,
            IEndpointWriteRepository endpoinWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpoindReadRepository = endpoinReadRepository;
            _endpoinWriteRepository = endpoinWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };

                await _menuWriteRepository.AddAsync(_menu);
            }

            Endpoint? endpoint = await _endpoindReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if (endpoint != null)
            {
                var action = _applicationService.GetAuthorizeDefinationsEndpoints(type)
                     .FirstOrDefault(m => m.Name == menu)
                     ?.Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Menu = _menu,
                    Code = action.Code,
                    ActionType = action.ActionType,
                    Id = Guid.NewGuid(),
                    Definition = action.Defination,
                    HttpType = action.HttpType,
                };

                await _endpoinWriteRepository.AddAsync(endpoint);
            }

            foreach (var role in endpoint.Roles)
            {
                endpoint.Roles.Remove(role);
            }

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Id)).ToListAsync();

            foreach (var roleId in appRoles)
            {
                endpoint.Roles.Add(roleId);
            }

            await _endpoinWriteRepository.SaveAsync();
            //TODO bu servis incelenecek düzgün çalışmıyor.
        }

        public async Task<GetRolesToEndpointQueryResponse> GetRolesToEndpoint(GetRolesToEndpointQueryRequest request)
        {
            var endpoint = await _endpoindReadRepository.Table.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Id == request.Id);

            if (endpoint is null)
            {
                throw new EndpointFailedException("Endpoint bulunamadı.");
            }

            IEnumerable<string> roles = endpoint.Roles.Select(r => r.Id);

            return new()
            {
                Roles = roles
            };

        }
    }
}
