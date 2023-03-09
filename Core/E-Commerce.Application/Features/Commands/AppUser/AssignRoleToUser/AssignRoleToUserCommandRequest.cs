using E_Commerce.Application.Features.Commands.AppUser.LoginUser;
using MediatR;

namespace E_Commerce.Application.Features.Commands.AppUser.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
    {
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}