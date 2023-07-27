using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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
        foreach (var tag in article.Tags)
            _ctxt.Tags.Attach(tag);
        _ctxt.Articles.Update(article); //sprawdzić czy nie trzeba ręcznie dodać tagsów
        await _ctxt.SaveChangesAsync();
    }

    public async Task<bool> IsInUse(string title) => await _ctxt.Articles.AnyAsync(x => x.Title == title);

    public async Task Add(Article article)
    {
        foreach(var tag in article.Tags) 
            _ctxt.Tags.Attach(tag);

        await _ctxt.Articles.AddAsync(article); //sprawdzić czy nie trzeba ręcznie dodać tagsów
        await _ctxt.SaveChangesAsync();
    }
}