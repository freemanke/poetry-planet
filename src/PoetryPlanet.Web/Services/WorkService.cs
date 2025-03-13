using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Web.Data;
using PoetryPlanet.Web.Data.Models;

namespace PoetryPlanet.Web.Services;

public class WorkService(ILogger<WorkService> logger, ApplicationDbContext db)
{
    public async Task<string[]> GetAllDynastyNamesAsync()
    {
        try
        {
            return (await db.Dynasties.OrderBy(a => a.StartYear).Select(a => a.Name)
                .ToArrayAsync()).Distinct().ToArray();
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    public async Task<string[]> GetAllAuthorNamesAsync()
    {
        try
        {
            return (await db.Authors.Select(a => a.Name)
                .ToArrayAsync()).Distinct().ToArray();
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    public IQueryable<Work> GetWorks(string keyword, IList<string> dynastyNames, IList<string> authorNames)
    {
        try
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
        catch (Exception ex)
        {
            return new List<Work>().AsQueryable();
        }
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
        try
        {
            await db.UserFavoriteWorks.Where(a => a.Username == username).ToListAsync();
        }
        catch (Exception ex)
        {
            return [];
        }

        return [];
    }
}
