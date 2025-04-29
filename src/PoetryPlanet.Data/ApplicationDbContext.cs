using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Data.ModelDtos;
using PoetryPlanet.Data.Models;
using Author = PoetryPlanet.Data.Models.Author;
using Collection = PoetryPlanet.Data.Models.Collection;
using CollectionKind = PoetryPlanet.Data.Models.CollectionKind;
using CollectionQuote = PoetryPlanet.Data.Models.CollectionQuote;
using CollectionWork = PoetryPlanet.Data.Models.CollectionWork;
using Dynasty = PoetryPlanet.Data.Models.Dynasty;
using Quote = PoetryPlanet.Data.Models.Quote;
using Work = PoetryPlanet.Data.Models.Work;

namespace PoetryPlanet.Data;

public class ApplicationDbContext(ILogger<ApplicationDbContext> logger, DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Dynasty> Dynasties { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionKind> CollectionKinds { get; set; }
    public DbSet<CollectionQuote> CollectionQuotes { get; set; }
    public DbSet<CollectionWork> CollectionWorks { get; set; }
    public DbSet<UserFavoriteWork> UserFavoriteWorks { get; set; }

    public void EnsuredInitialize(IMapper mapper)
    {
        if (Authors.Any())
        {
            logger.LogInformation("数据库已初始化，此次无需操作");
            return;
        }
        
        const string rootPath = "./Data/json";
        {
            var filePath = Path.Combine(rootPath, "authors.json");
            var json = File.ReadAllText(filePath)
                .Replace(": null,", ": \"\",")
                .Replace("\"views_count\" : \"\",", "\"views_count\" : 0,");
            var list = Serializer.Deserialize<AuthorList>(json);
            foreach (var item in list!.Items) Authors.Add(mapper.Map<Author>(item));
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "collection_kinds.json");
            var json = File.ReadAllText(filePath);
            var list = Serializer.Deserialize<CollectionKindList>(json);
            foreach (var item in list!.Items) CollectionKinds.Add(mapper.Map<CollectionKind>(item));
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "collection_quotes.json");
            var list = Serializer.Deserialize<CollectionQuoteList>(File.ReadAllText(filePath));
            foreach (var item in list!.Items) CollectionQuotes.Add(mapper.Map<CollectionQuote>(item));
            SaveChanges();
        } 
        {
            var filePath = Path.Combine(rootPath, "collection_works.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<CollectionWorkList>(json);
            foreach (var item in list!.Items) CollectionWorks.Add(mapper.Map<CollectionWork>(item));
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "collections.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<CollectionList>(json);
            foreach (var item in list!.Items) Collections.Add(mapper.Map<Collection>(item));
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "dynasties.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<DynastyList>(json);
            foreach (var item in list!.Items) Dynasties.Add(mapper.Map<Dynasty>(item));
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "quotes.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<QuoteList>(json);
            foreach (var item in list!.Items) Quotes.Add(mapper.Map<Quote>(item));
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "works.json"); 
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",")
                .Replace("\"posts_count\" : \"\",", "\"posts_count\" : 0,");
            var list = Serializer.Deserialize<WorkList>(json);
            foreach (var item in list!.Items) Works.Add(mapper.Map<Work>(item));
            SaveChanges();
        }
        
        logger.LogInformation("数据库数据初始化完成");
    }
}