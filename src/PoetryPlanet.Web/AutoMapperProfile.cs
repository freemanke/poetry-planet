using AutoMapper;
using Dtos=PoetryPlanet.Web.Data.ModelDtos;
using PoetryPlanet.Web.Data.Models;

namespace PoetryPlanet.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Dtos.Author, Author>();
            CreateMap<Dtos.CollectionKind, CollectionKind>();
            CreateMap<Dtos.CollectionQuote, CollectionQuote>();
            CreateMap<Dtos.CollectionWork, CollectionWork>();
            CreateMap<Dtos.Collection, Collection>();
            CreateMap<Dtos.Dynasty, Dynasty>();
            CreateMap<Dtos.Quote, Quote>();
            CreateMap<Dtos.Work, Work>();
            
            CreateMap<Author, Dtos.Author>();
            CreateMap<CollectionKind, Dtos.CollectionKind>();
            CreateMap<CollectionQuote, Dtos.CollectionQuote>();
            CreateMap<CollectionWork, Dtos.CollectionWork>();
            CreateMap<Collection, Dtos.Collection>();
            CreateMap<Dynasty, Dtos.Dynasty>();
            CreateMap<Quote, Dtos.Quote>();
            CreateMap<Work, Dtos.Work>();
        }
    }
}