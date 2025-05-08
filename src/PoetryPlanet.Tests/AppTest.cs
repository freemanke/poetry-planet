using System.Text.Json;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Tests;

public class AppTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Parse()
    {
        var items = JsonSerializer.Deserialize<List<WorkInfo>>(File.ReadAllText("./data/works.json"));
        Console.WriteLine(JsonSerializer.Serialize(items));
    }
}