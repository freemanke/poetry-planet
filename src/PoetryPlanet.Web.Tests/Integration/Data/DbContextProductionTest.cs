namespace PoetryPlanet.Web.Tests.Integration.Data;

public class DbContextTestProduction : DbIntegrationTestBase
{
    public DbContextTestProduction() : base("Production")
    {
    }

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