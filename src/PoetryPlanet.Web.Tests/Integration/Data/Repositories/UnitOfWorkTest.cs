
namespace PoetryPlanet.Web.Tests.Integration.Data.Repositories;

public class UnitOfWorkTest : IntegrationTestBase
{
    private readonly IUnitOfWork unit;

    public UnitOfWorkTest()
    {
        unit = GetRequiredService<IUnitOfWork>();
        Assert.That(unit, Is.Not.Null);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        unit.Dispose();
    }

    [Test]
    public async Task AddWithTransaction()
    {
        var transaction = await unit.BeginTransactionAsync();
        Assert.That(transaction, Is.Not.EqualTo(null));

        var name = Guid.NewGuid().ToString();
        var dynasty = new Dynasty { Name = name };
        unit.Dynasties.Add(dynasty);
        var result = await unit.SaveChangeAsync();
        if (result)
        {
            await transaction.CommitAsync();
            var find = await unit.Dynasties.FirstOrDefaultAsync(a => a.Name == name);
            Assert.That(find, Is.Not.Null);

            unit.Dynasties.Remove(find);
            await unit.SaveChangeAsync();
        }
        else await transaction.RollbackAsync();
    }

    [Test]
    public async Task ToListAsync()
    {
        var items = await unit.Dynasties.ToListAsync();
        Assert.That(items, Is.Not.Empty);
    }

    [Test]
    public async Task ToListAsyncOfEntityForTest()
    {
        var items = await unit.TestTestEntities.ToListAsync();
        Assert.That(items, Is.Empty);
    }

    [Test]
    public async Task CRUD()
    {
        var work = GetRequiredService<IUnitOfWork>();
        Assert.That(work, Is.Not.Null);

        var item = await work.Dynasties.FirstOrDefaultAsync(a => a.Name == "唐");
        Assert.That(item!.Name, Is.EqualTo("唐"));

        item = await work.Dynasties.FindAsync(10001);
        Assert.That(item!.Name, Is.EqualTo("宋"));

        var items = await work.Dynasties.ToListAsync();
        Assert.That(items, Is.Not.Null);

        var result = work.Dynasties.Where(a => a.Id > 0);
        Assert.That(result.ToList(), Is.Not.Null);
    }
}