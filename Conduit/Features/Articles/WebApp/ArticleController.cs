using Conduit.Features.Articles.Application.Commands;
using Conduit.Features.Articles.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Features.Articles.WebApp
{
    [Route("/api")]
        [ApiController]
        public class ArticleController : Controller
        {

        private readonly Create _create;
        private readonly IArticleQueriesRepo _articleQueriesRepo;

        public ArticleController(Create create, IArticleQueriesRepo articleQueriesRepo)
        {
            _create = create;
            _articleQueriesRepo = articleQueriesRepo;
        }

            [HttpPost("articles"), Authorize]
            public async Task<IActionResult> Create([FromBody] Create.ArticleCreateEnvelope article, CancellationToken cancellationToken = default)
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

        //[HttpPut("articles/tags")]
        //public async Task<IActionResult> Put(SetTagsFroArticlesCommand command)
        //{
        //    await _setTagsForArticles.Handle(command);
        //    return Ok("ok");
        //}
    }
}
