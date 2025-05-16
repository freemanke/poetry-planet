using AutoMapper;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;

namespace PoetryPlanet
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthorInfo, Author>();
            CreateMap<CollectionKindInfo, CollectionKind>();
            CreateMap<CollectionQuoteInfo, CollectionQuote>();
            CreateMap<CollectionWorkInfo, CollectionWork>();
            CreateMap<CollectionInfo, Collection>();
            CreateMap<DynastyInfo, Dynasty>();
            CreateMap<QuoteInfo, Quote>();
            CreateMap<WorkInfo, Work>();
            CreateMap<WorkListItemInfo, Work>();
            
            CreateMap<Author, AuthorInfo>();
            CreateMap<CollectionKind, CollectionKindInfo>();
            CreateMap<CollectionQuote, CollectionQuoteInfo>();
            CreateMap<CollectionWork, CollectionWorkInfo>();
            CreateMap<Collection, CollectionInfo>();
            CreateMap<Dynasty, DynastyInfo>();
            CreateMap<Quote, QuoteInfo>();
            CreateMap<Work, WorkInfo>();
            CreateMap<Work, WorkListItemInfo>();
        }
    }
}