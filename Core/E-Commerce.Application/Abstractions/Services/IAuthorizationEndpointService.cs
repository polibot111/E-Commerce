using E_Commerce.Application.Features.Queries.AssignRoleEndpoint.GetRolesToEndpoint;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type);

        Task<GetRolesToEndpointQueryResponse> GetRolesToEndpoint(GetRolesToEndpointQueryRequest request);
    }
}
