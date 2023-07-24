using Conduit.Features.Users.Application;
using Conduit.Features.Users.Application.Queries;
using Conduit.Infrastructure;
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
        private readonly ICurrentUser _currentUser;

        public UserController(IMediator mediator, ICurrentUser currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
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
        public async Task<UserEnvelope> GetCurrent(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCurrentUser.CurrentUser( _currentUser.GetCurrentId() ?? "<unknown>"), cancellationToken);
            return result;
        }
        //Dodać to potem
        //[HttpGet("profiles/{username}"), Authorize]
        //public Task<ProfileEnvelope> Get(string username)
        //{
        //    return _mediator.Send();
        //}
    }
}