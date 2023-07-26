using Conduit.Entities;
using Conduit.Features.Articles.Application.Dto;
using Conduit.Features.Articles.Application.Interfaces;

namespace Conduit.Features.Articles.Application.Commands
{
    public class Create
    {

        public class ArticleCreateRequest
        {
            public string? title { get; set; }
            public string? description { get; set; }
            public string? body { get; set; }
            public string[]? tagList { get; set; }
        }

        public record ArticleCreateEnvelope(ArticleCreateRequest article);

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IArticlesRepository _articlesRepository;
        private readonly ITagsRepository _tagsRepository;
        private readonly IPersonRepository _personRepository;

        public Create(IHttpContextAccessor httpContextAccessor
            ,IArticlesRepository articlesRepository
            ,ITagsRepository tagsRepository
            ,IPersonRepository personRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _articlesRepository = articlesRepository;
            _tagsRepository = tagsRepository;
            _personRepository = personRepository;
        }
        private async Task<bool> CheckTitile(string title)
        {
            try
            {
                var res = _articlesRepository.CheckTitle(title);
                return (res == null);
            } catch
            { 
            return false;
           }
        }

        private async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request, Person person, List<Tag> tags)
        {
            Article article = Article.CreateArticle(request, person);

            await _articlesRepository.AddArticle(article);
            await _articlesRepository.AddArticleTag(article, tags);
            return new ArticleEnvelope(article);
        }

            public async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request)
            {
                
            var sub = _httpContextAccessor.HttpContext?.User.FindFirst(type: "sud")?.Value;

            if (await CheckTitile(request.title)) 
            {
                throw new ArgumentException("Title is already in use");
            }

            var tags = new List<Tag>();
            foreach (var tag in (request.tagList ?? Enumerable.Empty<string>()))
            {
                var t = _tagsRepository.GetTag(tag);
                if (t == null)
                {
                    t = new Tag()
                    {
                        TagId = tag
                    };
                    _tagsRepository.UpdateTags(t);
                }
                tags.Add(t);
            }

            Person person = await _personRepository.GetPerson(sub);

            return await CreateArticle(request, person, tags);
            }
        // w samym obiekcie article zmieniamy tagi, tam jest sprawdzany obecny obiekt pobrany z bazy i w tedy jak zmienimy w obiekcie 
        // i przejdzie walidacje zapisujemy go
        }
    }