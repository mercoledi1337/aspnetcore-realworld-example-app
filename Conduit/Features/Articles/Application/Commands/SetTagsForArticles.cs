using Conduit.Features.Articles.Application.Interfaces;
using static Conduit.Features.Articles.Application.Commands.Create;

namespace Conduit.Features.Articles.Application.Commands
{


    public record SetTagsFroArticlesCommand(int Article, List<string> Tags);

        public class SetTagsForArticles
    {
        private readonly IArticlesRepository _articlesRepository;

        public SetTagsForArticles(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }
        public async Task Handle(SetTagsFroArticlesCommand command)
        {
            var article = _articlesRepository.GetArticle(command.Article);
            article.SetTags(command.Tags);
            await _articlesRepository.UpdateArticle(article);
        }
    }
}
