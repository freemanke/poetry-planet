using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Data;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Web.Controllers;

[Route("api/v1/collections")]
public class CollectionController(ApplicationDbContext db, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<List<CollectionInfo>> GetListAsync()
    {
        var items = await db.Collections.Select(a => mapper.Map<CollectionInfo>(a)).ToListAsync();
        return items;
    }
}