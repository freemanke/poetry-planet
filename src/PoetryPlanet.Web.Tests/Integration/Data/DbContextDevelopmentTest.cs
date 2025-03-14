namespace PoetryPlanet.Web.Tests.Integration.Data;

public class ContextDevelopmentTest : IntegrationTestBase
{
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        db.Dispose();
    }

    [Test]
    public override void EnsuredInitialize()
    {
       
        base.EnsuredInitialize();
    }
}