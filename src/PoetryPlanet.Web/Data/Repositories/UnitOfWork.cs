using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PoetryPlanet.Web;
using PoetryPlanet.Web.Data.Models;

[assembly: ExceptionInterceptor(AttributeTargetTypes = "PoetryPlanet.Web.Data.Repositories.*")]

namespace PoetryPlanet.Web.Data.Repositories;

public interface IDynastyRepository : IRepository<Dynasty>;
public interface IWorkRepository : IRepository<Work>;
public interface IQuoteRepository : IRepository<Quote>;
public interface ICollectionRepository : IRepository<Collection>;
public interface IAuthorRepository : IRepository<Author>;
public interface ITestEntityRepository : IRepository<TestEntity>;

public class DynastyRepository(ILogger logger, ApplicationDbContext context)
    : Repository<Dynasty>(logger, context), IDynastyRepository;

public class WorkRepository(ILogger logger, ApplicationDbContext context)
    : Repository<Work>(logger, context), IWorkRepository;

public class CollectionRepository(ILogger logger, ApplicationDbContext context)
    : Repository<Collection>(logger, context), ICollectionRepository;

public class QuoteRepository(ILogger logger, ApplicationDbContext context)
    : Repository<Quote>(logger, context), IQuoteRepository;

public class AuthorRepository(ILogger logger, ApplicationDbContext context)
    : Repository<Author>(logger, context), IAuthorRepository;

public class TestEntityRepository(ILogger logger, ApplicationDbContext context)
    : Repository<TestEntity>(logger, context), ITestEntityRepository;

public interface IUnitOfWork: IDisposable {
    IDynastyRepository Dynasties { get; }
    IWorkRepository Works { get; }
    ICollectionRepository Collections { get; }
    IQuoteRepository Quotes { get; }
    IAuthorRepository Authors { get; }
    ITestEntityRepository TestTestEntities { get; }
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<bool> SaveChangeAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ILogger logger;
    private readonly ApplicationDbContext context;

    public UnitOfWork(ILoggerFactory loggerFactory, ApplicationDbContext context)
    {
        this.context = context;
        logger = loggerFactory.CreateLogger<UnitOfWork>();
        Dynasties = new DynastyRepository(loggerFactory.CreateLogger<DynastyRepository>(), this.context);
        Works = new WorkRepository(loggerFactory.CreateLogger<WorkRepository>(), this.context);
        Collections = new CollectionRepository(loggerFactory.CreateLogger<CollectionRepository>(), this.context);
        Quotes = new QuoteRepository(loggerFactory.CreateLogger<QuoteRepository>(), this.context);
        Authors = new AuthorRepository(loggerFactory.CreateLogger<AuthorRepository>(), this.context);
        TestTestEntities = new TestEntityRepository(loggerFactory.CreateLogger<TestEntityRepository>(), this.context);
    }

    public IDynastyRepository Dynasties { get; }
    public IWorkRepository Works { get; }
    public ICollectionRepository Collections { get; }
    public IQuoteRepository Quotes { get; }
    public IAuthorRepository Authors { get; }
    public ITestEntityRepository TestTestEntities { get; }

    public void Dispose()
    {
        context.Dispose();
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
       return await context.Database.BeginTransactionAsync();
    }

    public async Task<bool> SaveChangeAsync()
    {
        await context.SaveChangesAsync();
        return true;
    }
}

public interface IRepository<T> where T : class
{
    Task<T?> FindAsync(int id);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    Task<List<T>> ToListAsync();
    Task<List<T>> ToListAsync(Expression<Func<T, bool>> expression);
    Task<List<TResult>> SelectToListAsync<TResult>(Expression<Func<T, TResult>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ILogger logger;
    protected readonly ApplicationDbContext context;

    protected Repository(ILogger logger, ApplicationDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        context.Set<T>().AddRange(entities);
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        context.Set<T>().RemoveRange(entities);
    }

    public async Task<List<TResult>> SelectToListAsync<TResult>(Expression<Func<T, TResult>> expression)
    {
        return await context.Set<T>().Select(expression).ToListAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Where(expression);
    }

    public async Task<List<T>> ToListAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> ToListAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().Where(expression).ToListAsync();
    }

    public async Task<T?> FindAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().FirstOrDefaultAsync(expression);
    }
}

/// <summary>
/// 单元测试用实体
/// </summary>
public class TestEntity
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("name")] public string Name { get; set; } = "";
}