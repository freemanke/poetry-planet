using System.Text.Json;
using PoetryPlanet.Data;
using PoetryPlanet.Dtos;
using PoetryPlanet.Services;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Tests;

public class AppTest
{
    [Test]
    public void DownloadSqlite()
    {
        var filePath = "/tmp/abc.sqlite";
        var httpService = App.GetRequiredService<HttpService>();
        httpService.Download(AppSetting.SQLiteUrl, filePath);
        Assert.That(File.Exists(filePath), Is.True);
    }
    
    [Test]
    public void DownloadSampleMp3()
    {
        var filePath = "/tmp/sample.mp3";
        var httpService = App.GetRequiredService<HttpService>();
        httpService.Download(AppSetting.SampleMp3Url, filePath);
        Assert.That(File.Exists(filePath), Is.True);
    }

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
    [Explicit]
    public void ReCreateSQLite()
    {
        var db = App.GetRequiredService<ApplicationDbContext>();
        if(File.Exists(AppSetting.SQLiteFilePath)) File.Delete(AppSetting.SQLiteFilePath);
        db.Database.EnsureCreated();
        db.EnsuredInitialize();
        Console.WriteLine(AppSetting.SQLiteFilePath);
        Assert.That(File.Exists(AppSetting.SQLiteFilePath), Is.True);
        Assert.That(db.Authors.Count(), Is.GreaterThan(10));
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
       var  stream = File.Open("./Assets/sample.mp3", FileMode.Open);
       Assert.That(stream, Is.Not.Null);
       stream.Close();
    }
}