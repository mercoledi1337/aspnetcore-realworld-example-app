using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;


public class ArticleCommandsRepo : IArticleCommandsRepo
{
    private readonly DataContext _ctxt;
    public ArticleCommandsRepo(DataContext ctxt)
    {
        _ctxt = ctxt;
    }
    public async Task<Article> Get(Guid id) => await _ctxt.Articles.Include(a => a.Tags)
            .Where(x => x.ArticleId == id).FirstOrDefaultAsync();

    public async Task Update(Article article)
    {
        var tmp = _ctxt.Articles.Include(x => x.Tags).FirstOrDefault(a => a.Title == article.Title);

        foreach (var tag in article.Tags)
            _ctxt.Tags.Attach(tag);

        _ctxt.Articles.Update(article);
        await _ctxt.SaveChangesAsync();
    }

    public async Task UpdateWithComments(Article article)
    {
        _ctxt.Articles.Update(article);
        await _ctxt.SaveChangesAsync();
    }

    public async Task Delete(Article article)
    {
        _ctxt.Articles.Update(article);
        await _ctxt.SaveChangesAsync();
    }

    public async Task<bool> IsInUse(string title) => await _ctxt.Articles.AnyAsync(x => x.Title == title);

    public async Task Add(Article article)
    {
        foreach(var tag in article.Tags) 
            _ctxt.Tags.Attach(tag);

        await _ctxt.Articles.AddAsync(article); 
        await _ctxt.SaveChangesAsync();
    }
}