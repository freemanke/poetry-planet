namespace PoetryPlanet.Web.Tests.Integration.Data;

public class UnitOfWorkTest : DbIntegrationTestBase
{
    public UnitOfWorkTest() : base("Development")
    {
    }

    [Test]
    public async Task CRUD()
    {
        var work = GetRequiredService<IUnitOfWork>();
        Assert.That(work, Is.Not.Null);
        
        var item = await work.Dynasty.FirstOrDefaultAsync(a => a.Name == "唐");
        Assert.That(item!.Name, Is.EqualTo("唐"));
        
        item = await work.Dynasty.FindAsync(10001);
        Assert.That(item!.Name, Is.EqualTo("宋"));
        
        var items = await work.Dynasty.ToListAsync();
        Assert.That(items, Is.Not.Null);

        items = work.Dynasty.Where(a => a.Id > 0);
        Assert.That(items.ToList(), Is.Not.Null);
    }
}