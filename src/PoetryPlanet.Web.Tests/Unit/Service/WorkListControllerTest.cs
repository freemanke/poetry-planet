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
}