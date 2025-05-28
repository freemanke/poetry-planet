using System.Text.Json;
using AutoMapper;
using Dapper;
using Microsoft.Data.Sqlite;
using PoetryPlanet.Data;
using PoetryPlanet.Data.Models;
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
    public void CreateSQLiteDatabase()
    {
        var db = App.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();
        db.EnsuredInitialize();
        Console.WriteLine(App.DatabaseFilePath);
        Assert.That(File.Exists(App.DatabaseFilePath), Is.True);
        Assert.That(db.Authors.Count(), Is.GreaterThan(10));
    }
    
    [Test]
    public void CreateSQLite()
    {
        var db = App.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();
        db.EnsuredInitialize();
        Assert.That(db.Authors.Count(), Is.GreaterThan(10));
    }

    [Test]
    public void Dapper()
    {
        var sqliteFilePath = Path.Combine(AppSetting.ConfigRootPath, "poetry-planet.sqlite");
        if(!File.Exists(sqliteFilePath)) File.Copy("poetry-planet.sqlite", sqliteFilePath);
        var connection = new SqliteConnection($"DataSource={sqliteFilePath};Cache=Shared");
        var items = connection.Query<AuthorInfo>("select * from authors");
        Assert.That(items.Count(), Is.GreaterThan(10));
        Console.WriteLine(Serializer.Serialize(items.First()));
    }

    [Test]
    public void ParseWorks()
    {
        var items = JsonSerializer.Deserialize<List<WorkInfo>>(File.ReadAllText("./data/works.json"));
        Console.WriteLine(JsonSerializer.Serialize(items));
    }

    [Test]
    public void ReadMp3()
    {
       var  MediaStream = File.Open("./Assets/sample.mp3", FileMode.Open);
    }
}