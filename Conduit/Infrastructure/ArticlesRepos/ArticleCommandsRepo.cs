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
    public async Task<Article> Get(Guid id)
    {
        return await _ctxt.Articles.Include(a => a.Tags).
            Where(x => x.ArticleId == id).FirstOrDefaultAsync();
    }

    public async Task Update(Article article)
    {
        _ctxt.Articles.Update(article); //sprawdzić czy nie trzeba ręcznie dodać tagsów
        await _ctxt.SaveChangesAsync();
    }

    public async Task Add(Article article)
    {
        await _ctxt.Articles.AddAsync(article); //sprawdzić czy nie trzeba ręcznie dodać tagsów
        await _ctxt.SaveChangesAsync();
    }
}