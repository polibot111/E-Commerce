using MediatR;

namespace E_Commerce.Application.Features.Queries.AssignRoleEndpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryRequest : IRequest<GetRolesToEndpointQueryResponse>
    {
        public Guid Id { get; set; }
    }
}