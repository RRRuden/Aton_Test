namespace Domain.Interfaces;

public interface IRepository<T>
{
    public Task Create(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
    public Task<T?> GetById(Guid id);
}