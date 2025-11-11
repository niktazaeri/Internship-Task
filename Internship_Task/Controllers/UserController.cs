using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.RefreshTokenFeatures.requests.commands;
using Internship_Task.Application.Features.UserFeatures.requests.commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Internship_Task.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            var command = new RegisterCommand { registerDTO =  registerDTO };
            var response = await _mediator.Send(command);
            if (!response.Success.HasValue || !response.Success.Value)
                return BadRequest(response);
            return Created("",response);

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDTO)
        {
            var command = new LoginCommand { loginDTO = loginDTO };
            var response = await _mediator.Send(command);
            if(!response.Success.Value || !response.Success.HasValue)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> CheckRefreshToken([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            var command = new ValidateRefreshTokenCommand { RefreshTokenDTO = refreshTokenDTO };
            var response = await _mediator.Send(command);
            if (!response.Success.HasValue || !response.Success.Value)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
