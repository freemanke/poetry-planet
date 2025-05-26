using System.Text.Json;
using AutoMapper;
using PoetryPlanet.Data;
using PoetryPlanet.Dtos;
using PoetryPlanet.ViewModels;
using PoetryPlanet.Views;

namespace PoetryPlanet.Tests;

public class AppTest
{
    [Test]
    public void GetRequiredService()
    {
        var mainView = App.GetRequiredService<MainViewModel>();
        Assert.That(mainView, Is.Not.Null);
    }
    
    [Test]
    public void SQLite()
    {
        var db = App.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();
        db.EnsuredInitialize();
        Assert.That(db.Authors.Count(), Is.GreaterThan(10));
    }

    [Test]
    public void Parse()
    {
        var items = JsonSerializer.Deserialize<List<WorkInfo>>(File.ReadAllText("./data/works.json"));
        Console.WriteLine(JsonSerializer.Serialize(items));
    }

    [Test]
    public void Read()
    {
       var  MediaStream = File.Open("./Assets/sample.mp3", FileMode.Open);
    }
}