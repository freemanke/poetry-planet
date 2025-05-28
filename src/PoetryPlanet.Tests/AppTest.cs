using System.Text.Json;
using AutoMapper;
using Dapper;
using Microsoft.Data.Sqlite;
using PoetryPlanet.Data;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;
using PoetryPlanet.Services;
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
        Assert.That(App.GetRequiredService<ApplicationDbContext>(), Is.Not.Null);
        Assert.That(App.GetRequiredService<PoetryService>(), Is.Not.Null);
    }
    
    [Test]
    public void EnsureSQLite()
    {
        Assert.That(File.Exists(AppSetting.SQLiteFilePath), Is.True);
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
        if(!File.Exists(AppSetting.SQLiteFilePath)) File.Copy(AppSetting.SQLiteFileName, AppSetting.SQLiteFilePath);
        var connection = new SqliteConnection($"DataSource={AppSetting.SQLiteFilePath};Cache=Shared");
        var items = connection.Query("select id as Id, title as Title, content as Content from works").ToWorks();
        Assert.That(items, Has.Count.GreaterThan(10));
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