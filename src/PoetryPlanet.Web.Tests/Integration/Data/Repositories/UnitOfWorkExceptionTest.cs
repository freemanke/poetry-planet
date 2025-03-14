namespace PoetryPlanet.Web.Tests.Integration.Data.Repositories;

/// <summary>
/// 用于测试异常情况下，所有方法调用的异常处理均由 <see cref="ExceptionInterceptor"/> 接管处理，
/// 不抛出任何异常到调用方，且所有方法在发生异常后，都返回默认值。
/// </summary>
public class UnitOfWorkExceptionTest : IntegrationTestBase
{
    private readonly IUnitOfWork unit;

    public UnitOfWorkExceptionTest():base("WrongDatabase")
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
    public async Task FindAsync()
    {
        var items = await unit.Dynasties.FindAsync(10001);
        Assert.That(items, Is.EqualTo(null));
    }

    [Test]
    public async Task FirstOrDefaultAsync()
    {
        var items = await unit.Dynasties.FirstOrDefaultAsync(a => a.Id > 0);
        Assert.That(items, Is.EqualTo(null));
    }

    [Test]
    public async Task ToListAsync()
    {
        var items = await unit.Dynasties.ToListAsync();
        Assert.That(items, Is.Empty);

        items = await unit.Dynasties.ToListAsync(a => a.Id > 0);
        Assert.That(items, Is.Empty);
    }

    [Test]
    public async Task SelectToListAsync()
    {
        var items = await unit.Dynasties.SelectToListAsync(a => a.Id > 0);
        Assert.That(items, Is.Empty);
    }

    [Test]
    public async Task Add()
    {
        unit.Dynasties.Add(new Dynasty());
        var result = await unit.SaveChangeAsync();
        Assert.That(result, Is.EqualTo(false));
    }
    
    [Test]
    public async Task  AddRange()
    {
        unit.Dynasties.AddRange([new Dynasty()]);
        var result = await unit.SaveChangeAsync();
        Assert.That(result, Is.EqualTo(false));
    }
    
    [Test]
    public async Task  Remove()
    {
        var dynasty = new Dynasty();
        unit.Dynasties.Remove(dynasty);
        var result = await unit.SaveChangeAsync();
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public async Task RemoveWithTransaction()
    {
        var transaction = await unit.BeginTransactionAsync();
        Assert.That(transaction, Is.EqualTo(null));
    }

    [Test]
    public async Task  RemoveRange()
    {
        var dynasties = new[] { new Dynasty() };
        unit.Dynasties.RemoveRange(dynasties);
        var result = await unit.SaveChangeAsync();
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void Where()
    {
        var items = unit.Dynasties.Where(a => a.Id > 0);
        Assert.That(items, Is.Not.Null);
        Console.WriteLine(items.Expression);
    }
}