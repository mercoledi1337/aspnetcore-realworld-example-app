using Conduit.Entities;
using Conduit.Features.Articles.Application.Commands;
using Conduit.Features.Users.Application;
using Conduit.Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Features.Articles.WebApp
{
        [Route("/api")]
        [ApiController]
        public class ArticleController : Controller
        {
            private readonly IMediator _mediator;


        public ArticleController(IMediator mediator)
            {
                _mediator = mediator;
        }

            [HttpPost("articles"), Authorize]
            public async Task<IActionResult> Create([FromBody] Create.CreateingArticle command, CancellationToken cancellationToken = default)
            {
                var result = await _mediator.Send(command, cancellationToken);

                return Ok(result);
            }
    }
}
