using MediatR;

namespace E_Commerce.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandRequest: IRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}