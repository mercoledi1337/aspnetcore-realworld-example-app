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
            public List<string>? tagList { get; set; }
        }

        public record ArticleCreateEnvelope(ArticleCreateRequest article);

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPersonRepository _personRepository;
        private readonly IArticleCommandsRepo _articleCommandsRepo;
        private readonly ITagsQueries _tagsQueries;

        public Create(IHttpContextAccessor httpContextAccessor
            ,IPersonRepository personRepository
            ,IArticleCommandsRepo articleCommandsRepo
            ,ITagsQueries tagsQueries)
        {
            _httpContextAccessor = httpContextAccessor;
            _personRepository = personRepository;
            _articleCommandsRepo = articleCommandsRepo;
            _tagsQueries = tagsQueries;
        }
        //private async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request, Person person, List<Tag> tags)
        //{
        //    Article article = Article.CreateArticle(request, person);

        //    await _articlesRepository.AddArticle(article);
        //    await _articlesRepository.AddArticleTag(article, tags);
        //    return new ArticleEnvelope(article);
        //}

        private async Task<List<Tag>> CheckTags(List<string> tags)
        {
            var result = new List<Tag>();
            foreach (var tag in tags)
            {
                var tmp = await _tagsQueries.CheckTags(tag);
                if (tmp == null)
                {
                    var t = new Tag()
                    {
                        Name = tag
                    };
                    result.Add(t);
                }
                else
                {
                    result.Add(tmp);
                }
            }
            return result;
        }
        public async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request, List<string> tags)
            {
                
            var sub = _httpContextAccessor.HttpContext?.User.FindFirst(type: "sud")?.Value;

            Person person = await _personRepository.GetPerson(sub);
            Article article = Article.Create(person);
            article.SetArticleDetails(request, person);
            var tagsTmp = await CheckTags(tags);
            article.SetTags(tagsTmp);
            if (await _articleCommandsRepo.IsInUse(article.Title))
                throw new ArgumentException("title in use");
            await _articleCommandsRepo.Add(article);
            
            return new ArticleEnvelope(article);
            }
        }
    }