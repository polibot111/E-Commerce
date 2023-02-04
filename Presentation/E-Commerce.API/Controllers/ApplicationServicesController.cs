using E_Commerce.Application.Abstractions.Services.Configuration;
using E_Commerce.Application.CustomAttributes;
using E_Commerce.Application.Enıums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefination(ActionType = ActionType.Reading, Definition ="Get Authorize Definition Endpoints", Menu = "Application Services")]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var datas = _applicationService.GetAuthorizeDefinationsEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
