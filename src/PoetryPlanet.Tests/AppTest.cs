using System.Text.Json;
using PoetryPlanet.Dtos;
using PoetryPlanet.Views;

namespace PoetryPlanet.Tests;

public class AppTest
{
    [Test]
    public void GetRequiredService()
    {
        var mainView = App.GetRequiredService<MainView>();
        Assert.That(mainView, Is.Not.Null);
    }

    [Test]
    public void Parse()
    {
        var items = JsonSerializer.Deserialize<List<WorkInfo>>(File.ReadAllText("./data/works.json"));
        Console.WriteLine(JsonSerializer.Serialize(items));
    }
}