using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Abstractions.Services.Configuration;
using E_Commerce.Application.Repositories.ElementsRepositories;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Repositories.ElementsServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepository _endpoindReadRepository;
        readonly IEndpointWriteRepository _endpoinWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpoinReadRepository,
            IEndpointWriteRepository endpoinWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository)
        {
            _applicationService = applicationService;
            _endpoindReadRepository = endpoinReadRepository;
            _endpoinWriteRepository = endpoinWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
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

                await _menuWriteRepository.SaveAsync();
            }



            Endpoint? endpoint = await _endpoindReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if (endpoint == null)
            {
               var action = _applicationService.GetAuthorizeDefinationsEndpoints(type)
                    .FirstOrDefault(m => m.Name == menu)
                    ?.Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {                                 
                    Menu = _menu,
                    Code = action.Code,
                    ActionType= action.ActionType,
                    Id = Guid.NewGuid(),
                    Definition = action.Defination,
                    HttpType = action.HttpType,
                };



                    await _endpoinWriteRepository.AddAsync(endpoint);
                    await _endpoinWriteRepository.SaveAsync();
              
              
            }
            foreach (var role in roles)
            {
                endpoint.Roles.Add(new()
                {
                    Id = role
                });
            }
            await _endpoinWriteRepository.SaveAsync();


        }
    }
}
