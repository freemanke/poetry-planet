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

    public void EnsuredInitialize(IMapper? mapper = null)
    {
        Database.EnsureCreated();
        if (Authors.Any())
        {
            logger.LogInformation("数据库已初始化，此次无需操作");
            return;
        }
        
        const string rootPath = "./json";
        {
            var filePath = Path.Combine(rootPath, "authors.json");
            var json = File.ReadAllText(filePath)
                .Replace(": null,", ": \"\",")
                .Replace("\"views_count\" : \"\",", "\"views_count\" : 0,");
            var list = Serializer.Deserialize<AuthorList>(json);
            foreach (var item in list!.Items) Authors.Add(new Author
            {
                Id = item.Id,BaiduWiki = item.BaiduWiki,
                BirthYear = item.BirthYear,
                DeathYear = item.DeathYear,
                Dynasty = item.Dynasty,
                Intro = item.Intro,
                IntroTr = item.IntroTr,
                Name = item.Name,
                NameTr = item.NameTr,
                QuotesCount = item.QuotesCount, 
                UpdatedAt = item.UpdatedAt, 
                WorksCount = item.WorksCount,
                WorksShiCount = item.WorksShiCount,
                WorksCiCount = item.WorksCiCount,
                WorksWenCount = item.WorksWenCount,
                WorksFuCount = item.WorksFuCount,
                RemoteId = item.RemoteId, 
                WorksQuCount = item.WorksQuCount, 
                ViewsCount = item.ViewsCount
            });
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "collection_kinds.json");
            var json = File.ReadAllText(filePath);
            var list = Serializer.Deserialize<CollectionKindList>(json);
            foreach (var item in list!.Items) CollectionKinds.Add(new CollectionKind
            {
                Id = item.Id,
                Name = item.Name,
                NameTr = item.NameTr,
                Limit = item.Limit,
            });
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "collection_quotes.json");
            var list = Serializer.Deserialize<CollectionQuoteList>(File.ReadAllText(filePath));
            foreach (var item in list!.Items) CollectionQuotes.Add(new CollectionQuote()
            {
                Id = item.Id,
                CollectionKindId = item.CollectionKindId,
                Quote = item.Quote,
                QuoteAuthor = item.QuoteAuthor,
                QuoteAuthorTr = item.QuoteAuthorTr,
                QuoteTr = item.QuoteTr,
                QuoteWork = item.QuoteWork,
                QuoteWorkId = item.QuoteWorkId,
                QuoteWorkTr = item.QuoteWorkTr,
                ShowOrder = item.ShowOrder,
                CollectionId = item.CollectionId, 
                QuoteId = item.QuoteId,
            });
            SaveChanges();
        } 
        {
            var filePath = Path.Combine(rootPath, "collection_works.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<CollectionWorkList>(json);
            foreach (var item in list!.Items) CollectionWorks.Add(new CollectionWork
            {
                Id = item.Id,
                CollectionId = item.CollectionId,
                ShowOrder = item.ShowOrder,
                WorkId = item.WorkId,
                WorkTitle = item.WorkTitle,
                WorkTitleTr = item.WorkTitleTr,
                WorkFullTitle = item.WorkFullTitle,
                WorkFullTitleTr = item.WorkFullTitleTr,
                WorkAuthor = item.WorkAuthor,
                WorkAuthorTr = item.WorkAuthorTr,
                WorkDynasty = item.WorkDynasty, 
                Collection = item.Collection, 
                CollectionTr = item.CollectionTr,
                WorkContent = item.WorkContent,
                WorkContentTr = item.WorkContentTr, 
                WorkDynastyTr = item.WorkDynastyTr, 
                WorkKind = item.WorkKind,
                
            });
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "collections.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<CollectionList>(json);
            foreach (var item in list!.Items) Collections.Add(new Collection
            {
                Id = item.Id,
                Cover = item.Cover,
                KindId = item.KindId,
                Kind = item.Kind,
                KindTr = item.KindTr,
                Name = item.Name,
                NameTr = item.NameTr,
                OnlineData = item.OnlineData,
                QuotesCount = item.QuotesCount,
                ShortDesc = item.ShortDesc,
                ShortDescTr = item.ShortDescTr,
                ShowOrder = item.ShowOrder,
                WorksCount = item.WorksCount, 
                Desc = item.Desc,
                DescTr = item.DescTr, 
            });
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "dynasties.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<DynastyList>(json);
            foreach (var item in list!.Items) Dynasties.Add(new Dynasty
            {
                Id = item.Id,
                Name = item.Name,
                NameTr = item.NameTr,
                EndYear = item.EndYear,
                Intro = item.Intro,
                IntroTr = item.IntroTr,
                StartYear = item.StartYear, 
            });
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "quotes.json");
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",");
            var list = Serializer.Deserialize<QuoteList>(json);
            foreach (var item in list!.Items) Quotes.Add(new Quote
            {
                Id = item.Id,
                Author = item.Author,
                AuthorId = item.AuthorId,AuthorTr = item.AuthorTr, Dynasty = item.Dynasty,
                Kind = item.Kind,
                QuoteTr = item.QuoteTr,
                WorkId = item.WorkId,
                WorkTitle = item.WorkTitle,
                WorkTitleTr = item.WorkTitleTr,
                DynastyTr = item.DynastyTr, 
                QuoteText = item.QuoteText,
                UpdatedAt = item.UpdatedAt
            });
            SaveChanges();
        }
        {
            var filePath = Path.Combine(rootPath, "works.json"); 
            var json = File.ReadAllText(filePath).Replace(": null,", ": \"\",")
                .Replace("\"posts_count\" : \"\",", "\"posts_count\" : 0,");
            var list = Serializer.Deserialize<WorkList>(json);
            foreach (var item in list!.Items)
                Works.Add(new Work
                {
                    Id = item.Id,
                    Annotation = item.Annotation,
                    AnnotationTr = item.AnnotationTr,
                    Author = item.Author,
                    AuthorDesc = item.AuthorDesc,
                    AuthorDescTr = item.AuthorDescTr,
                    AuthorId = item.AuthorId,
                    AuthorRemoteId = item.AuthorRemoteId,
                    AuthorTr = item.AuthorTr,
                    Content = item.Content,
                    ContentTr = item.ContentTr,
                    Dynasty = item.Dynasty,
                    DynastyTr = item.DynastyTr,
                    Foreword = item.Foreword,
                    ForewordTr = item.ForewordTr,
                    HighlightedAt = item.HighlightedAt,
                    Intro = item.Intro,
                    IntroTr = item.IntroTr,
                    Kind = item.Kind,
                    KindCn = item.KindCn,
                    KindCnTr = item.KindCnTr,
                    Layout = item.Layout,
                    MasterComment = item.MasterComment,
                    MasterCommentTr = item.MasterCommentTr,
                    PostsCount = item.PostsCount,
                    QuotesCount = item.QuotesCount,
                    Title = item.Title,
                    TitleTr = item.TitleTr,
                    Translation = item.Translation,
                    TranslationTr = item.TranslationTr, 
                    ShowOrder = item.ShowOrder, 
                    BaiduWiki = item.BaiduWiki,
                    AuthorWorksCount = item.AuthorWorksCount,
                    CollectionsCount = item.CollectionsCount,
                });
            SaveChanges();
        }
        
        logger.LogInformation("数据库数据初始化完成");
    }
}