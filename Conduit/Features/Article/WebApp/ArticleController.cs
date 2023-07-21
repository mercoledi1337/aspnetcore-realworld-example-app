using Conduit.Features.Users.Application.Queries;
using Conduit.Features.Users.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Features.Article.WebApp
{
    public class ArticleController : Controller
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

            [HttpPost("articles")]
            public async Task<IActionResult> Create([FromBody] Register.RegisteringUser command, CancellationToken cancellationToken = default)
            {
                var result = await _mediator.Send(command, cancellationToken);

                return Ok(result);
            }

        }
    }
}
