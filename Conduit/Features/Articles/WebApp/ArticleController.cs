using Conduit.Entities;
using Conduit.Features.Articles.Application.Commands;
using Conduit.Features.Articles.Application.Dto;
using Conduit.Features.Articles.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Conduit.Features.Articles.Application.Commands.Create;
using static Conduit.Features.Articles.Application.Commands.Delete;
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
        private readonly Delete _delete;
        private readonly ITagsQueries _tagsQueries;
        private readonly List _list;

        public ArticleController(Create create, IArticleQueriesRepo articleQueriesRepo
            , Update update, Delete delete, ITagsQueries tagsQueries
            , List list
            )
        {
            _create = create;
            _articleQueriesRepo = articleQueriesRepo;
            _update = update;
            _delete = delete;
            _tagsQueries = tagsQueries;
            _list = list;
        }

            [HttpPost("articles"), Authorize]
            public async Task<IActionResult> Create([FromBody] ArticleCreateEnvelope article, CancellationToken cancellationToken = default)
            {
            var result = await _create.CreateArticle(article.article, article.article.tagList);

                return Ok();
            }

        [HttpGet("articles"), Authorize]
        public async Task<ArticlesEnvelope> GetArticles()
        {

            return new ArticlesEnvelope()
            {
                Articles = await _list.GetAll(),
                //dodac liczenie artykułow
                ArticlesCount = 3
                };
        }

        [HttpGet("articles/{slug}"), Authorize]
        public async Task<List<ArticleReadModel>> GetArticle(string slug)
        {
            return await _articleQueriesRepo.GetList(slug);
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
            await _delete.DelateComment(title, comment);
            return Ok("ok");
        }

        [HttpGet("tags"), Authorize]
        public async Task<TagsEnvelope> GetTags() => new TagsEnvelope(_tagsQueries.GetAll());


        //[HttpPut("articles/tags"), Authorize]
        //public async Task<IActionResult> Put([FromBody] ArticleCreateEnvelope article)
        //{
        //    await _update.UpdateTags(article.article, article.article.tagList);
        //    return Ok("ok");
        //}


        //[HttpDelete("articles/tags"), Authorize]
        //public async Task<IActionResult> Delate([FromBody] ArticleDeleteRequest article)
        //{
        //    await _delete.DelateTag(article, article.tag);
        //    return Ok("ok");
        //}
    }
}