using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Commands.AppUser.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
    {
        readonly IUserService _userService;

        public AssignRoleToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.AssiginRoleToUserAsync(request.UserId, request.Roles);
            return new AssignRoleToUserCommandResponse();
        }
    }
}
