namespace PoetryPlanet.Web.Tests.Integration.Data;

public class ContextTestProduction() : IntegrationTestBase("Production")
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