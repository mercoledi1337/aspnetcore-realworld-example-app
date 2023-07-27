using Conduit.Features.Articles.Application.Commands;
using Conduit.Features.Articles.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Conduit.Features.Articles.Application.Commands.Create;
using static Conduit.Features.Articles.Application.Commands.Update;

namespace Conduit.Features.Articles.WebApp
{
    [Route("/api")]
        [ApiController]
        public class ArticleController : Controller
        {

        private readonly Create _create;
        private readonly IArticleQueriesRepo _articleQueriesRepo;
        private readonly Update _update;

        public ArticleController(Create create, IArticleQueriesRepo articleQueriesRepo, Update update)
        {
            _create = create;
            _articleQueriesRepo = articleQueriesRepo;
            _update = update;
        }

            [HttpPost("articles"), Authorize]
            public async Task<IActionResult> Create([FromBody] ArticleCreateEnvelope article, CancellationToken cancellationToken = default)
            {
            var result = await _create.CreateArticle(article.article, article.article.tagList);

                return Ok();
            }

        [HttpGet("articles"), Authorize]
        public async Task<IActionResult> GetArticles()
        {

            var result = await _articleQueriesRepo.GetAll();
            return Ok(result);
        }

        [HttpPut("articles/tags"), Authorize]
        public async Task<IActionResult> Put([FromBody] ArticleCreateEnvelope article)
        {
            await _update.UpdateArticle(article.article, article.article.tagList);
            return Ok("ok");
        }
    }
}
