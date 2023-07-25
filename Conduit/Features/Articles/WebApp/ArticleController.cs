using Conduit.Features.Articles.Application.Commands;
using Conduit.Features.Articles.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Features.Articles.WebApp
{
        [Route("/api")]
        [ApiController]
        public class ArticleController : Controller
        {

        private readonly Create _create;

        public ArticleController(Create create)
        {
            _create = create;
        }

            [HttpPost("articles"), Authorize]
            public async Task<IActionResult> Create([FromBody] Create.ArticleCreateEnvelope article, CancellationToken cancellationToken = default)
            {
            var result = await _create.CreateArticle(article.article);

                return Ok(result);
            }

        //[HttpGet("articles"), Authorize]
        //public async Task<IActionResult> GetArticles([FromQuery] string tag , [FromQuery] string author, [FromQuery] string favorited, [FromQuery] int? limit, [FromQuery] int? offset, CancellationToken cancellationToken = default)
        //{
        //    var result = await _mediator.Send(, cancellationToken);

        //    return Ok(result);
        //}

        [HttpPost("articles/tags")]
        public async Task<IActionResult> Put(List<string> tags)
        {
            return Ok();
        }
    }
}
