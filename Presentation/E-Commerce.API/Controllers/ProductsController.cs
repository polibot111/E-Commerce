using E_Commerce.Application.Features.Queries.GetAllProduct;
using E_Commerce.Application.Repositories.ElementsRepositories;
using E_Commerce.Application.RequestParameters;
using E_Commerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly ILogger<ProductsController> _logger;
        readonly IMediator _mediator;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {

            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
           var response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }




    }
}
