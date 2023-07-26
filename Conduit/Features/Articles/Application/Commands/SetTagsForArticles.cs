using Azure.Core;
using Conduit.Entities;
using Conduit.Features.Articles.Application.Dto;
using Conduit.Features.Articles.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using static Conduit.Features.Articles.Application.Commands.Create;

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
            //nie mogę wyciągnąć z artykułów jego tagów
            var currentTags = _tagsRepository.GetArticleTag(command.Article);
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
