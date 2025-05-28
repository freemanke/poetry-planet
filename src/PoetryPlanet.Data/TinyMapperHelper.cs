using Nelibur.ObjectMapper;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Data;

public static class TinyMapperHelper
{
    public static void Init()
    {
        TinyMapper.Bind<AuthorInfo, Author>();
        TinyMapper.Bind<CollectionKindInfo, CollectionKind>();
        TinyMapper.Bind<CollectionQuoteInfo, CollectionQuote>();
        TinyMapper.Bind<CollectionWorkInfo, CollectionWork>();
        TinyMapper.Bind<CollectionInfo, Collection>();
        TinyMapper.Bind<DynastyInfo, Dynasty>();
        TinyMapper.Bind<QuoteInfo, Quote>();
        TinyMapper.Bind<WorkInfo, Work>();
        TinyMapper.Bind<WorkListItemInfo, Work>();
            
        TinyMapper.Bind<Author, AuthorInfo>();
        TinyMapper.Bind<CollectionKind, CollectionKindInfo>();
        TinyMapper.Bind<CollectionQuote, CollectionQuoteInfo>();
        TinyMapper.Bind<CollectionWork, CollectionWorkInfo>();
        TinyMapper.Bind<Collection, CollectionInfo>();
        TinyMapper.Bind<Dynasty, DynastyInfo>();
        TinyMapper.Bind<Quote, QuoteInfo>();
        TinyMapper.Bind<Work, WorkInfo>();
        TinyMapper.Bind<Work, WorkListItemInfo>();
    }
}