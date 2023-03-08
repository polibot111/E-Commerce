using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.DTOs.User;
using E_Commerce.Application.Exceptions;
using E_Commerce.Application.Features.Commands.AppUser.CreateUser;
using E_Commerce.Application.Helpers;
using E_Commerce.Application.RequestParameters;
using E_Commerce.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IConfiguration _configuration;
        public UserService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

   

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO createUserDTO)
        {
            var result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUserDTO.UserName,
                Email = createUserDTO.Email,
                NameSurName = createUserDTO.NameSurName,
            }, createUserDTO.Password);

            CreateUserResponseDTO response = new() { Succeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarılı bir şekilde oluşturuldu.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }
            }
            return response;
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFaildException();
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpireDate = accessTokenDate.AddSeconds(Convert.ToInt32(_configuration["RefreshToken:AddOnToTokensExpireTime"]));
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new NotFoundUserException();
            }

        }
        public async Task<List<ListUserDTO>> GetAllUsersAsync(Pagination pagination)
        {
            var users = await _userManager.Users.Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToListAsync();

            return users.Select(user => new ListUserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                NameSurName = user.NameSurName,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,

            }).ToList();
        }

        public async Task<int> GetAllUserCount()
        {
            var result = await Task.Run(() =>
            {
                return _userManager.Users.Count();
            });
         
            return result;

        }

    }
}
