using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Abstractions.Token;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = E_Commerce.Domain.Entities.Identity;

namespace E_Commerce.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;
        readonly ILogger<LoginUserCommandHandler> _logger;
        public LoginUserCommandHandler(IAuthService authService, ILogger<LoginUserCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password);

            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }
    }
}
