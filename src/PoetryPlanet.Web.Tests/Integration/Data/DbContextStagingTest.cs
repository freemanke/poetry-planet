namespace PoetryPlanet.Web.Tests.Integration.Data;

public class DbContextStagingTest : DbIntegrationTestBase
{
    public DbContextStagingTest() : base("Staging")
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