using System.Text.Json;
using PoetryPlanet.Web.Controllers;

namespace PoetryPlanet.Web.Tests.Unit.Service;

public class WorkListControllerTest : UnitTestBase
{
    [Test]
    public async Task GetWorkListAsync()
    {
        var controller = GetRequiredService<WorkListController>();
        var result = await controller.GetWorkListAsync();
        Console.WriteLine(JsonSerializer.Serialize(result));
        Assert.That(result, Has.Count.GreaterThan(1));
    }
}