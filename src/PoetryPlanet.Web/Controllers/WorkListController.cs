using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Data;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Web.Controllers;

[Route("api/v1/work_list")]
public class WorkListController(ApplicationDbContext db, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<List<WorkListItemInfo>> GetWorkListAsync(int count = 200)
    {
        count = count < 1 ? 1 : count;
        var items = await db.Works.Take(count).Select(a => new WorkListItemInfo
        {
            Id = a.Id,
            Title = a.Title,
            Author = a.Author ?? "",
            AuthorId = a.AuthorId ?? 0,
            Dynasty = a.Dynasty ?? "",
            Content = a.Content ?? "".Split("。", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? ""
        }).ToListAsync();
        return items;
    }
}