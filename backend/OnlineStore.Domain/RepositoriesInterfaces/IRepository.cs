namespace OnlineStore.Domain.RepositoriesInterfaces;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetById(Guid Id,CancellationToken ctsToken = default);
    public Task<IReadOnlyList<TEntity>> GetAll(CancellationToken ctsToken = default);
    public Task Add(TEntity entity,CancellationToken ctsToken = default);
    public Task Update(TEntity entity,CancellationToken ctsToken = default);
    public Task DeleteById(Guid id, CancellationToken ctsToken = default);
    public Task Delete(CancellationToken ctsToken = default);
}