using PoetryPlanet.Web.Controllers;

namespace PoetryPlanet.Web.Tests.Unit.Service;

public class CollectionControllerTest : UnitTestBase
{
    [Test]
    public async Task GetWorkListAsync()
    {
        var controller = GetRequiredService<CollectionController>();
        var result = await controller.GetListAsync();
        Console.WriteLine( Serializer.Serialize(result));
        Assert.That(result, Has.Count.GreaterThan(1));
    }
}