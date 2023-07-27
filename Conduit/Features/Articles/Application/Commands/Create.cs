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
            public List<Tag>? tagList { get; set; }
        }

        public record ArticleCreateEnvelope(ArticleCreateRequest article);

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPersonRepository _personRepository;
        private readonly IArticleCommandsRepo _articleCommandsRepo;

        public Create(IHttpContextAccessor httpContextAccessor
            ,IPersonRepository personRepository
            ,IArticleCommandsRepo articleCommandsRepo)
        {
            _httpContextAccessor = httpContextAccessor;

            _personRepository = personRepository;
            _articleCommandsRepo = articleCommandsRepo;
        }
        

        //private async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request, Person person, List<Tag> tags)
        //{
        //    Article article = Article.CreateArticle(request, person);

        //    await _articlesRepository.AddArticle(article);
        //    await _articlesRepository.AddArticleTag(article, tags);
        //    return new ArticleEnvelope(article);
        //}

            public async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request, List<Tag> tags)
            {
                
            var sub = _httpContextAccessor.HttpContext?.User.FindFirst(type: "sud")?.Value;

            Person person = await _personRepository.GetPerson(sub);
            Article article = Article.Create();
            article.SetArticleDetails(request, person);
            article.SetTags(tags);
            await _articleCommandsRepo.Add(article);
            //if (await CheckTitile(request.title)) 
            //{
            //    throw new ArgumentException("Title is already in use");
            //}

            //var tags = new List<Tag>();
            //foreach (var tag in (request.tagList ?? Enumerable.Empty<string>()))
            //{
            //    var t = await _tagsRepository.GetTag(tag);
            //    if (t == null)
            //    {
            //        t = new Tag()
            //        {
            //            TagId = tag
            //        };
            //        await _tagsRepository.UpdateTags(t);
            //    }
            //    tags.Add(t);
            //}


            return new ArticleEnvelope(article);
            }
        // w samym obiekcie article zmieniamy tagi, tam jest sprawdzany obecny obiekt pobrany z bazy i w tedy jak zmienimy w obiekcie 
        // i przejdzie walidacje zapisujemy go
        }
    }