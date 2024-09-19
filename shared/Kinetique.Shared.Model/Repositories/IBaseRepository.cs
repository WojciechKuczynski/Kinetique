namespace Kinetique.Shared.Model.Repositories;

public interface IBaseRepository<T> where T: BaseModel
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T?> Get(long id);
    public Task<T> Add(T obj);
    public Task Update(T obj);
}