using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Data;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Web.Controllers;

[Route("api/v1/works")]
public class WorkController(ApplicationDbContext db, IMapper mapper) : Controller
{
    [HttpGet]
    [Route("")]
    public async Task<List<WorkInfo>> GetAsync(int count)
    {
        count = count <= 0 ? 100 : count;
        var items = await db.Works.Take(count).Select(a => mapper.Map<WorkInfo>(a)).ToListAsync();
        return items;
    }
}