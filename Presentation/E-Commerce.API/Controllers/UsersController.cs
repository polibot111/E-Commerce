using E_Commerce.Application.CustomAttributes;
using E_Commerce.Application.Enıums;
using E_Commerce.Application.Features.Commands.AppUser.AssignRoleToUser;
using E_Commerce.Application.Features.Commands.AppUser.CreateUser;
using E_Commerce.Application.Features.Commands.AppUser.LoginUser;
using E_Commerce.Application.Features.Commands.AppUser.UpdatePassword;
using E_Commerce.Application.Features.Queries.AppUser.GetAllUsers;
using E_Commerce.Application.Features.Queries.AppUser.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommandRequest createUserCommandRequest)
        {
            var response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            var response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefination(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUserQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefination(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefination(ActionType = ActionType.Writing, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
