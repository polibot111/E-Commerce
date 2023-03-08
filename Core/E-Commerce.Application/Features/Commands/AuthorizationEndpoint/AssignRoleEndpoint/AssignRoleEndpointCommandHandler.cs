using E_Commerce.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointQueryHandler : IRequestHandler<AssignRoleEndpointQueryRequest, AssignRoleEndpointQueryResponse>
    {
        readonly IAuthorizationEndpointService _authorizationEndpointService;

        public AssignRoleEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRoleEndpointQueryResponse> Handle(AssignRoleEndpointQueryRequest request, CancellationToken cancellationToken)
        {
            await _authorizationEndpointService.AssignRoleEndpointAsync(request.Roles, request.Menu, request.Code, request.Type);

            return new()
            {
                ProcessState = true
            };
        }
    }
}
