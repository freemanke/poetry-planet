using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Web.Data.Models;

namespace PoetryPlanet.Web.Data;

public interface IDynastyRepository : IRepository<Dynasty>;
public interface IWorkRepository : IRepository<Work>;
public interface IQuoteRepository : IRepository<Quote>;
public interface ICollectionRepository : IRepository<Collection>;
public interface IAuthorRepository : IRepository<Author>;

public class DynastyRepository : Repository<Dynasty>, IDynastyRepository
{
    public DynastyRepository(ILogger logger, ApplicationDbContext context) : base(logger, context)
    {
    }
}

public class WorkRepository : Repository<Work>, IWorkRepository
{
    public WorkRepository(ILogger logger, ApplicationDbContext context) : base(logger, context)
    {
    }
}

public class CollectionRepository : Repository<Collection>, ICollectionRepository
{
    public CollectionRepository(ILogger logger, ApplicationDbContext context) : base(logger, context)
    {
    }
}

public class QuoteRepository : Repository<Quote>, IQuoteRepository
{
    public QuoteRepository(ILogger logger, ApplicationDbContext context) : base(logger, context)
    {
    }
}

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(ILogger logger, ApplicationDbContext context) : base(logger, context)
    {
    }
}

public interface IUnitOfWork: IDisposable {
    IDynastyRepository Dynasties { get; }
    IWorkRepository Works { get; }
    ICollectionRepository Collections { get; }
    IQuoteRepository Quotes { get; }
    IAuthorRepository Authors { get; }
    bool Save();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ILogger logger;
    private ApplicationDbContext context;

    public UnitOfWork(ILoggerFactory loggerFactory, ApplicationDbContext context)
    {
        this.context = context;
        logger = loggerFactory.CreateLogger<UnitOfWork>();
        Dynasties = new DynastyRepository(loggerFactory.CreateLogger<DynastyRepository>(), this.context);
        Works = new WorkRepository(loggerFactory.CreateLogger<WorkRepository>(), this.context);
        Collections = new CollectionRepository(loggerFactory.CreateLogger<CollectionRepository>(), this.context);
        Quotes = new QuoteRepository(loggerFactory.CreateLogger<QuoteRepository>(), this.context);
        Authors = new AuthorRepository(loggerFactory.CreateLogger<AuthorRepository>(), this.context);
    }

    public IDynastyRepository Dynasties { get; }
    public IWorkRepository Works { get; }
    public  ICollectionRepository Collections { get; }
    public IQuoteRepository Quotes { get; }
    public IAuthorRepository Authors { get; }

    public void Dispose()
    {
        context.Dispose();
    }

    public bool Save()
    {
        try
        {
            context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        return false;
    }
}

public interface IRepository<T> where T : class
{
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    Task<T?> FindAsync(int id);
    Task<IEnumerable<T>> ToListAsync();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    IQueryable<T> OderBy(Expression<Func<T, bool>> expression);
    Task<List<TResult>> SelectToListAsync<TResult>(Expression<Func<T, TResult>> expression);
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

    public IQueryable<T> OderBy(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().OrderBy(expression);
    }

    public void Add(T entity)
    {
        try
        {
            context.Set<T>().Add(entity);
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }
    }

    public void AddRange(IEnumerable<T> entities)
    {
        try
        {
            context.Set<T>().AddRange(entities);
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }
    }

    public void Remove(T entity)
    {
        try
        {
            context.Set<T>().Remove(entity);
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        try
        {
            context.Set<T>().RemoveRange(entities);
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }
    }

    public async Task<List<TResult>> SelectToListAsync<TResult>(Expression<Func<T, TResult>> expression)
    {
        try
        {
            return await context.Set<T>().Select(expression).ToListAsync();
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }

        return await Task.FromResult(new List<TResult>());
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Where(expression);
    }

    public async Task<IEnumerable<T>> ToListAsync()
    {
        try
        {
            return await context.Set<T>().ToListAsync();
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }

        return new List<T>();
    }

    public async Task<T?> FindAsync(int id)
    {
        try
        {
            return await context.Set<T>().FindAsync(id);
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }

        return null;
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        try
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }
        catch (Exception e)
        {
            logger.LogError($"调用方法 {nameof(ToListAsync)} 时出错，{e.Message}");
        }

        return null;
    }
}