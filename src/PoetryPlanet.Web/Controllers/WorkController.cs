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
    public async Task<WorkInfo?> GetWorkAsync(int id)
    {
        var find = await db.Works.FirstOrDefaultAsync(a => a.Id == id);
        return find == null ? null : mapper.Map<WorkInfo>(find);
    }
}