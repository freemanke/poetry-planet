using Microsoft.Extensions.Logging;
using PoetryPlanet.Web.Data.Models;

namespace PoetryPlanet.Web.Tests.Integration.Data;

public class DbContextDevelopmentTest : DbIntegrationTestBase
{
    public DbContextDevelopmentTest() : base("Development")
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