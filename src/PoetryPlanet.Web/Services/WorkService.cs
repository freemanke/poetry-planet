using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Web;
using PoetryPlanet.Web.Data;
using PoetryPlanet.Web.Data.Models;
using PoetryPlanet.Web.Data.Repositories;

[assembly: ExceptionInterceptor(AttributeTargetTypes = $"{nameof(PoetryPlanet.Web.Services.DebugService)}", AspectPriority = 10)]

namespace PoetryPlanet.Web.Services;

public class WorkService(ILogger<WorkService> logger, IUnitOfWork unit, ApplicationDbContext db)
{
    public async Task<string[]> GetAllDynastyNamesAsync()
    {
        var items = (await db.Dynasties.OrderBy(a => a.StartYear).Select(a => a.Name)
            .ToArrayAsync()).Distinct().ToArray();
        logger.LogInformation($"获取到朝代信息 {Serializer.Serialize(items)}");
        return items;
    }

    public async Task<string[]> GetAllAuthorNamesAsync()
    {
        return (await unit.Authors.SelectToListAsync(a => a.Name)).Distinct().ToArray();
    }

    public IQueryable<Work> GetWorks(string keyword, IList<string> dynastyNames, IList<string> authorNames)
    {
        var query = db.Works.Where(a => a.Id > 0);
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(a =>
                a.Author.Contains(keyword)
                || a.Content.Contains(keyword)
                || a.Intro.Contains(keyword));
        }

        if (dynastyNames.Count != 0)
        {
            query = query.Where(a => dynastyNames.Contains(a.Dynasty));
        }

        if (authorNames.Count != 0)
        {
            query = query.Where(a => authorNames.Contains(a.Author));
        }

        return query.OrderBy(a => a.Id).AsQueryable();
    }

    public async Task ToggleFavoriteAsync(string username, int workId)
    {
        var userFavorite = await db.UserFavoriteWorks.FirstOrDefaultAsync(a => a.WorkId == workId);
        if (userFavorite == null)
        {
            userFavorite = new UserFavoriteWork { Username = username, WorkId = workId };
            db.UserFavoriteWorks.Add(userFavorite);
            await db.SaveChangesAsync();
            return;
        }

        db.UserFavoriteWorks.Remove(userFavorite);
        await db.SaveChangesAsync();
    }

    public async Task<List<UserFavoriteWork>> GetAllUserFavoriteWorksAsync(string username)
    {
        return await db.UserFavoriteWorks.Where(a => a.Username == username).ToListAsync();
    }
}
