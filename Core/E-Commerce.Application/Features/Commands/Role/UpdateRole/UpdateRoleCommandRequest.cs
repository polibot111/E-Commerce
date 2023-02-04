using E_Commerce.Application.Features.Commands.Role.CreateRole;
using MediatR;

namespace E_Commerce.Application.Features.Commands.Role.UpdateRole
{
    public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}