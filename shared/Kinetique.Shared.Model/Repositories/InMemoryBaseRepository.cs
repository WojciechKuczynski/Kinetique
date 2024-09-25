namespace Kinetique.Shared.Model.Repositories;

public abstract class InMemoryBaseRepository<T> : IBaseRepository<T> where T: BaseModel
{
    protected readonly List<T> _objects = [];
    
    public virtual Task<IEnumerable<T>> GetAll()
        => Task.FromResult(_objects.Select(x => x));

    public virtual Task<T?> Get(long id)
        => Task.FromResult(_objects.FirstOrDefault(x => x.Id == id));

    public virtual Task<T> Add(T obj)
    {
        _objects.Add(obj);
        
        return Task.FromResult(obj);
    }

    public virtual Task Update(T obj)
    {
        var objInSet = Get(obj.Id).Result;
        if (objInSet != null)
        {
            _objects.Remove(objInSet);
            _objects.Add(obj);
        }

        return Task.CompletedTask;
    }
}