using System.Text.Json;
using PoetryPlanet.Web.Controllers;

namespace PoetryPlanet.Web.Tests.Unit.Service;

public class WorkControllerTest : UnitTestBase
{
    [Test]
    public async Task GetWorkAsync()
    {
        var controller = GetRequiredService<WorkController>();
        var find = await controller.GetWorkAsync(10001);
        Console.WriteLine(JsonSerializer.Serialize(find));
        Assert.That(find!.Id, Is.EqualTo(10001));
    }
    
    [Test]
    public async Task GetWorksAsync()
    {
        var controller = GetRequiredService<WorkController>();
        var items = await controller.GetAsync();
        Console.WriteLine(JsonSerializer.Serialize(items));
        Assert.That(items.Count, Is.GreaterThan(100));
    }
}