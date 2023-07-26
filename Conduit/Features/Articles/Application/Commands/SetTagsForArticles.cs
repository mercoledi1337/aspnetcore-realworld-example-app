using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;


namespace Conduit.Features.Articles.Application.Commands
{

    public record SetTagsFroArticlesCommand(int Article, List<string> Tags);

        public class SetTagsForArticles
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly ITagsRepository _tagsRepository;

        public SetTagsForArticles(IArticlesRepository articlesRepository, ITagsRepository tagsRepository)
        {
            _articlesRepository = articlesRepository;
            _tagsRepository = tagsRepository;
        }
        public async Task Handle(SetTagsFroArticlesCommand command)
        {
            var article = _articlesRepository.GetArticle(command.Article);
            //chcemy wrzucić do encji i potem po przejściu validacji jej użyć
            article.SetTags(command.Tags);
            //sprawdzić czy tagi są w użyciu
            var tags = new List<Tag>();
            foreach (var tag in (command.Tags ?? Enumerable.Empty<string>()))
            {
                var t = _tagsRepository.GetTag(tag);
                if (t == null)
                {
                    t = new Tag()
                    {
                        TagId = tag
                    };
                    _tagsRepository.UpdateTags(t);
                    tags.Add(t);
                }
            }

            _tagsRepository.UpdateArticleTags(article, tags);
            //TagList.AddRange(tags.Except(TagList));
            
            await _articlesRepository.UpdateArticle(article);
        }
    }
}
