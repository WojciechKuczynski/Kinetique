using Microsoft.EntityFrameworkCore;

namespace Kinetique.Shared.Model.Repositories;

public class PostgresRepositoryBase<T>(DbContext context) : IBaseRepository<T>
    where T : BaseModel
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<T> _objects = context.Set<T>();

    public async Task<IEnumerable<T>> GetAll()
        => await _objects.ToListAsync();

    public async Task<T?> Get(long id)
        => await _objects.FindAsync(id);

    public async Task<T> Add(T obj)
    {
        var added = await _objects.AddAsync(obj);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task Update(T obj)
    {
        _objects.Update(obj);
        _objects.Entry(obj).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}