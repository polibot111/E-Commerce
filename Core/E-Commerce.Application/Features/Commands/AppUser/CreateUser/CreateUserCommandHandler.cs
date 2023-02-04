using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = E_Commerce.Domain.Entities.Identity;

namespace E_Commerce.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.CreateAsync(new()
            {
                Email= request.Email,
                UserName= request.UserName,
                NameSurName= request.NameSurName,
                Password= request.Password,
                PasswordConfirm= request.PasswordConfirm
            });

            return new()
            {
                Message = response.Message,
                Succeded= response.Succeded,
            };
        }

        
    }
}
