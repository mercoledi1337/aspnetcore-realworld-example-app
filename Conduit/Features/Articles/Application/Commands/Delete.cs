using Conduit.Entities;
using Conduit.Features.Articles.Application.Dto;
using Conduit.Features.Articles.Application.Interfaces;
using static Conduit.Features.Articles.Application.Commands.Update;

namespace Conduit.Features.Articles.Application.Commands
{
    public class Delete
    {
        public class ArticleDeleteRequest
        {
            public string? title { get; set; }
            public Tag tag { get; set; }
        }

        private readonly IArticleCommandsRepo _articleCommandsRepo;
        private readonly IArticleQueriesRepo _articleQueriesRepo;

        public Delete(
            IArticleCommandsRepo articleCommandsRepo
            , IArticleQueriesRepo articleQueriesRepo)
        {
            _articleCommandsRepo = articleCommandsRepo;
            _articleQueriesRepo = articleQueriesRepo;
        }

        public async Task<ArticleEnvelope> DelateTag(ArticleDeleteRequest request, Tag tag)
        {
            var article = await _articleQueriesRepo.Get(request.title);
            article.DeleteTag(tag);
            await _articleCommandsRepo.Delete(article);
            return new ArticleEnvelope(article);
        }

        public async Task<ArticleEnvelope> DelateComment(string articleName, Guid commentId)
        {
            var article = await _articleQueriesRepo.Get(articleName);
            article.DeleteComment(commentId);
            await _articleCommandsRepo.Delete(article);
            return new ArticleEnvelope(article);
        }
    }
}
