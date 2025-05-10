using PoetryPlanet.Services;

namespace PoetryPlanet.Tests;

public class PoetryServiceTest
{
    [Test]
    public void GetWorkList()
    {
        var service = App.GetRequiredService<PoetryService>();
        var lists =  service.GetWorkList();
        Assert.That(lists, Has.Count.GreaterThan(1));
    }
    
    [Test]
    public void GetWork()
    {
        var service =  App.GetRequiredService<PoetryService>();
        var work =  service.GetWork(10001);
        Assert.That(work.Id, Is.EqualTo(10001));
    }
}