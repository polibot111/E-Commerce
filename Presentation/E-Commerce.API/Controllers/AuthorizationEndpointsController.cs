using E_Commerce.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using E_Commerce.Application.Features.Queries.AssignRoleEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRolesToEndpoint([FromRoute]GetRolesToEndpointQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointQueryRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
            AssignRoleEndpointQueryResponse response =
            await _mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(response);
        }
    }
}
