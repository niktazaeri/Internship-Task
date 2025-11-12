using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.ProductFeatures.requests.commands;
using Internship_Task.Application.Features.ProductFeatures.requests.queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Internship_Task.Controllers
{
    [Route("api/products")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var response = await _mediator.Send(query);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct(int id)
        {
            if(id == 0 || id == null)
            {
                return BadRequest(new {Message = "Invalid id!"});
            }
            var query = new GetProductQuery { Id = id };
            var response = await _mediator.Send(query);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            var command = new CreateProductCommand
            {
                createProductDTO = createProductDTO,
                UserId = HttpContext.User.Claims.ToList()[0].Value
            };
            var response = await _mediator.Send(command);
            if (!response.Success)
                return BadRequest(response);
            return Created("",response);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var get_product_query = new GetProductQuery { Id = id };
            var get_product_query_response = await _mediator.Send(get_product_query);
            if (!get_product_query_response.Success)
            {
                return NotFound(get_product_query_response);
            }
            else
            {
                var userId = HttpContext.User.Claims.ToList()[0].Value;
                if (userId != get_product_query_response.Product.UserId)
                    return Forbid();
                var command = new DeleteProductCommand { productDTO = get_product_query_response.Product };
                var response = await _mediator.Send(command);
                if (!response.Success)
                    return NotFound(response);
                return NoContent();
            }  
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditProduct([FromBody] UpdateProductDTO updateProductDTO , int id)
        {
            var get_product_query = new GetProductQuery { Id = id };
            var get_product_query_response = await _mediator.Send(get_product_query);
            if (!get_product_query_response.Success)
            {
                return NotFound(get_product_query_response);
            }
            else
            {
                var userId = HttpContext.User.Claims.ToList()[0].Value;
                if (userId != get_product_query_response.Product.UserId)
                    return Forbid();
                var update_command = new UpdateProductCommand { updateProductDTO = updateProductDTO , Id=id };
                var update_command_response = await _mediator.Send(update_command);
                if (!update_command_response.Success)
                    return BadRequest(update_command_response);
                return Ok(update_command_response);
            }
            
        }
    }
}
