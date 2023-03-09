using E_Commerce.Application.DTOs.User;
using E_Commerce.Application.RequestParameters;
using E_Commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO createUserDTO);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUserDTO>> GetAllUsersAsync(Pagination pagination);
        Task<int> GetAllUserCount();
        Task AssiginRoleToUserAsync(string userId, IEnumerable<string> roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
    }
}
