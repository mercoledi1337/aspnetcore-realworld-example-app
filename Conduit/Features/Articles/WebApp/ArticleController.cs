using Conduit.Entities;
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
            await _update.UpdateTags(article.article, article.article.tagList);
            return Ok("ok");
        }


        [HttpDelete("articles/tags"), Authorize]
        public async Task<IActionResult> Delate([FromBody] ArticleDeleteRequest article)
        {
            await _update.DelateTag(article, article.tag);
            return Ok("ok");
        }

        [HttpPut("articles/comments"), Authorize]

        public async Task<IActionResult> Update(string title)
        {
            await _update.UpdateArticleWithComments(title);
            return Ok("ok");
        }

        [HttpDelete("articles/comment"), Authorize]
        public async Task<IActionResult> DelateComment(string title, Guid comment)
        {
            await _update.DelateComment(title, comment);
            return Ok("ok");
        }
    }
}
