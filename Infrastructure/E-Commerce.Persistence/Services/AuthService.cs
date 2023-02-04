using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Exceptions;
using E_Commerce.Application.Features.Commands.AppUser.LoginUser;
using T = E_Commerce.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Application.Abstractions.Token;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.WebUtilities;
using E_Commerce.Application.Helpers;

namespace E_Commerce.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly SignInManager<T.AppUser> _signInManager;
        readonly UserManager<T.AppUser> _userManager;
        readonly IUserService _userService;
        readonly ITokenHandler _tokenHandler;
        readonly IMailService _mailService;

        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler, IUserService userService, IMailService mailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
                throw new NotFoundUserException();


            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration);
                return token;
            }

            throw new AuthenticationErrorException();
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(y => y.RefreshToken == refreshToken && y.RefreshTokenExpireDate >= DateTime.UtcNow);

            if (user != null)
            {
                Token token = _tokenHandler.CreateAccessToken(user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration);
                return token;
            }
            throw new NotFoundUserException();
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,"ResetPassword", resetToken);
            }
            return false;
        }
    }
}
