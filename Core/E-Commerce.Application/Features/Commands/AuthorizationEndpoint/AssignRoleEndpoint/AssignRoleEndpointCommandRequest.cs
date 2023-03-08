using MediatR;

namespace E_Commerce.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointQueryRequest: IRequest<AssignRoleEndpointQueryResponse>
    {
        public string[] Roles { get; set; }
        public string Code { get; set; }
        public string Menu { get; set; }
        public Type? Type { get; set; }
    }
}