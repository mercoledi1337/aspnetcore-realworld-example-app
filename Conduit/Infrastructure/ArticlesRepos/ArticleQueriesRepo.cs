using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Features.Users.Application;
using Conduit.Features.Users.Application.Dto;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.ArticlesRepos
{
    public class ArticleQueriesRepo : IArticleQueriesRepo
    {
        private readonly DataContext _ctxt;
        public ArticleQueriesRepo(DataContext ctxt)
        {
            _ctxt = ctxt;
        }
        public async Task<List<ArticleReadModel>> GetAll()
        {
            var articles = await _ctxt.Articles
                .Include(a => a.Tags)
                .Include(x => x.Comments)
                .AsNoTracking()
                .Select(a => new ArticleReadModel
                {
                    Slug = a.Slug,
                    Title = a.Title,
                    Description = a.Description,
                    Body = a.Body,
                    TagList = a.Tags.Select(x => x.Name).ToList(),
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    author = new UserDto()
                        { 
                            Email = a.Author.Email,
                            Username = a.Author.Username
                        }
                })
                .ToListAsync();

            var result = articles
                .OrderByDescending(x => x.CreatedAt)
                .Skip(0)
                .Take(10)
                .ToList();

            return result;
        }

        public async Task<ArticleReadModel> GetArticle(string slug)
        {
            var article = await _ctxt.Articles
                .Include(a => a.Tags)
                .Include(x => x.Comments)
                .AsNoTracking()
                .Where(x => x.Slug == slug)
                .Select(a => new ArticleReadModel
                {
                    Slug = a.Slug,
                    Title = a.Title,
                    Description = a.Description,
                    Body = a.Body,
                    TagList = a.Tags.Select(x => x.Name).ToList(),
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    author = new UserDto()
                    {
                        Email = a.Author.Email,
                        Username = a.Author.Username
                    }
                })
                .FirstOrDefaultAsync();

            return article;
        }

        public async Task<Article> Get(string title) => await _ctxt.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Title == title);

        public async Task<List<ArticleReadModel>> GetList(string title)
        {
            var articles = await _ctxt.Articles
                .Include(a => a.Tags)
                .Include(x => x.Comments)
                .AsNoTracking()
                .Select(a => new ArticleReadModel
                {
                    Slug = a.Slug,
                    Title = a.Title,
                    Description = a.Description,
                    Body = a.Body,
                    TagList = a.Tags.Select(x => x.Name).ToList(),
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    author = new UserDto()
                    {
                        Email = a.Author.Email,
                        Username = a.Author.Username
                    }
                }).Where(x => x.TagList.Contains(title))
                .ToListAsync();

            var result = articles
                .OrderByDescending(x => x.CreatedAt)
                .Skip(0)
                .Take(10)
                .ToList();

            return result;
        }
    }
}
