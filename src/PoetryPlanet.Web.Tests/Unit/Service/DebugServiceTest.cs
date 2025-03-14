namespace PoetryPlanet.Web.Tests.Unit.Service;

public class DebugServiceTest : UnitTestBase
{
    private readonly DebugService debugService;

    public DebugServiceTest()
    {
        debugService = GetRequiredService<DebugService>();
    }
    
    [Test]
    public void ReturnVoid()
    {
        debugService.ReturnVoid();
    }

    [Test]
    public void ReturnScaleTypeInt()
    {
        var intValue = debugService.ReturnScaleTypeInt();
        Assert.That(intValue, Is.EqualTo(Activator.CreateInstance<int>()));
    }

    [Test]
    public void ReturnScaleTypeFloat()
    {
        var intValue = debugService.ReturnScaleTypeFloat();
        Assert.That(intValue, Is.EqualTo(Activator.CreateInstance<float>()));
    }

    [Test]
    public void ReturnReferenceTypeString()
    {
        var result = debugService.ReturnReferenceTypeString();
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ReturnReferenceTypeType()
    {
        var result = debugService.ReturnReferenceTypeType();
        Assert.That(result, Is.Null);
    }


    [Test]
    public void ReturnNullableString()
    {
        var result = debugService.ReturnNullableString();
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ReturnNullableInt()
    {
        var result = debugService.ReturnNullableInt();
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ReturnReferenceTypeListOfString()
    {
        var items = debugService.ReturnReferenceTypeListOfString();
        Assert.That(items, Is.Empty);
        Assert.That(items.GetType(), Is.EqualTo(typeof(List<string>)));
    }

    [Test]
    public void ReturnReferenceTypeListOfInt()
    {
        var items = debugService.ReturnReferenceTypeListOfInt();
        Assert.That(items, Is.Empty);
        Assert.That(items.GetType(), Is.EqualTo(typeof(List<int>)));
    }
    
    [Test]
    public async Task ReturnVoidAsync()
    {
       await debugService.ReturnVoidAsync();
    }
    
    [Test]
    public async Task ReturnScaleTypeIntAsync()
    {
        var value = await debugService.ReturnScaleTypeIntAsync();
        Assert.That(value, Is.EqualTo(0));
        Assert.That(value.GetType(), Is.EqualTo(typeof(int)));
    }
    
    [Test]
    public async Task ReturnNullableIntAsync()
    {
        var value = await debugService.ReturnNullableIntAsync();
        Assert.That(value, Is.Null);
    }
    
    [Test]
    public async Task ReturnReferenceTypeListOfIntAsync()
    {
        var items = await debugService.ReturnReferenceTypeListOfIntAsync();
        Assert.That(items, Is.Empty);
        Assert.That(items.GetType(), Is.EqualTo(typeof(List<int>)));
    }
}