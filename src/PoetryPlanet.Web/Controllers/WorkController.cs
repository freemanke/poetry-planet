using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Data;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Web.Controllers;

[Route("api/v1/works")]
public class WorkController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;

    public WorkController(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    [HttpGet]
    [Route("")]
    public async Task<GetWorkResponse> GetAsync(int count)
    {
        count = count <= 0 ? 100 : count;
        var items = await db.Works.Take(count).Select(a => mapper.Map<WorkInfo>(a)).ToListAsync();
        return new GetWorkResponse { Works = items };
    }
}