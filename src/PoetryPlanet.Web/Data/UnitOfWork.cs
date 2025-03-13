using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PoetryPlanet.Web.Data.Models;

namespace PoetryPlanet.Web.Data;

public interface IDynastyRepository : IRepository<Dynasty>;

public class DynastyRepository : Repository<Dynasty>, IDynastyRepository
{
    public DynastyRepository(ILogger<DynastyRepository> loggerFactory, ApplicationDbContext context) 
        : base(loggerFactory, context)
    {
    }
}

public interface IUnitOfWork: IDisposable {
    IDynastyRepository Dynasty { get; }
    bool Save();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ILogger logger;
    private ApplicationDbContext context;

    public UnitOfWork(ILoggerFactory loggerFactory, ApplicationDbContext context)
    {
        logger = loggerFactory.CreateLogger<UnitOfWork>();
        this.context = context;
        Dynasty = new DynastyRepository(loggerFactory.CreateLogger<DynastyRepository>(), this.context);
    }

    public IDynastyRepository Dynasty { get; }

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
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ILogger<Repository<T>> logger;
    protected readonly ApplicationDbContext context;

    protected Repository(ILogger<Repository<T>> logger, ApplicationDbContext context)
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
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().FirstOrDefaultAsync(expression);
    }
}