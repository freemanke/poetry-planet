namespace PoetryPlanet.Web.Tests.Unit.Data;

public class ApplicationDbContextTest : UnitTestBase
{
	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
		db.Database.EnsureDeleted();
		db.Database.EnsureCreated();
		Assert.That(db.Authors.Count(), Is.EqualTo(0));
	}

	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		db.Database.EnsureDeleted();
		db.Dispose();
	}

	[Test]
	public void EnsuredInitialize()
	{
        Log($"数据库信息：InMemoryDb");
        Log("正在初始化数据库...");
		db.EnsuredInitialize(GetRequiredService<IMapper>());
        Log("数据库初始化完成");
		Assert.That(db.Authors.Count(), Is.Not.EqualTo(0));
		
		var userFavorite = new UserFavoriteWork { WorkId = 100, Username = "freemanke" };
		db.UserFavoriteWorks.Add(userFavorite);
		db.SaveChanges();
		db.UserFavoriteWorks.Remove(userFavorite);
		db.SaveChanges();
	}
}