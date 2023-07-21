using Conduit.Features.Users.Application;
using Conduit.Features.Users.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Conduit.Features.Users.UI
{
    [Route("/api")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("users")]
        public async Task<IActionResult> Register([FromBody] Register.RegisteringUser command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPost("users/login")]
        public async Task<IActionResult> Login([FromBody] Authentication.AuthenticationUser command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPost("admin-panel"), Authorize(policy: "is-admin")]
        public IActionResult TestingRoleReturnigJustOk()
        {

            return Ok();
        }

        [HttpPut("user"), Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] Update.UpdateUser command)
        {

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("user"), Authorize]
        public async Task<UserEnvelope> GetCurrentUser()
        {
            //to później przeniść 
            var sub = HttpContext?.User.FindFirst(type: "sud")?.Value;
            var result = await _mediator.Send(new GetCurrentUser.CurrentUser(sub));
            return result;
        }
    }
}