using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.DTOs.Role;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
           var result = await _roleManager.CreateAsync(new AppRole() { Id= Guid.NewGuid().ToString() ,Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            return await Task.Run(() => _roleManager.Roles.Select(x => new RoleDTO
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList());
           
        }

        public async Task<RoleDTO> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) role = new AppRole();
            return new()
            {
                Id= role.Id,
                Name = role.Name,
            };


        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            appRole.Name= name;
            var result = await _roleManager.UpdateAsync(appRole);
            return result.Succeeded;
        }
    }
}
