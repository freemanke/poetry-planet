using AutoMapper;
using Dtos=PoetryPlanet.Data.ModelDtos;
using Author = PoetryPlanet.Data.ModelDtos.Author;
using Collection = PoetryPlanet.Data.ModelDtos.Collection;
using CollectionKind = PoetryPlanet.Data.ModelDtos.CollectionKind;
using CollectionQuote = PoetryPlanet.Data.ModelDtos.CollectionQuote;
using CollectionWork = PoetryPlanet.Data.ModelDtos.CollectionWork;
using Dynasty = PoetryPlanet.Data.ModelDtos.Dynasty;
using Quote = PoetryPlanet.Data.ModelDtos.Quote;
using Work = PoetryPlanet.Data.ModelDtos.Work;

namespace PoetryPlanet.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, Author>();
            CreateMap<CollectionKind, CollectionKind>();
            CreateMap<CollectionQuote, CollectionQuote>();
            CreateMap<CollectionWork, CollectionWork>();
            CreateMap<Collection, Collection>();
            CreateMap<Dynasty, Dynasty>();
            CreateMap<Quote, Quote>();
            CreateMap<Work, Work>();
            
            CreateMap<Author, Author>();
            CreateMap<CollectionKind, CollectionKind>();
            CreateMap<CollectionQuote, CollectionQuote>();
            CreateMap<CollectionWork, CollectionWork>();
            CreateMap<Collection, Collection>();
            CreateMap<Dynasty, Dynasty>();
            CreateMap<Quote, Quote>();
            CreateMap<Work, Work>();
        }
    }
}