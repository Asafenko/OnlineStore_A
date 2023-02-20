namespace OnlineStore.Domain.RepositoriesInterfaces;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetById(Guid Id,CancellationToken cts = default);
    public Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cts = default);
    public Task Add(TEntity entity,CancellationToken cts = default);
    public Task Update(TEntity entity,CancellationToken cts = default);
    public Task DeleteById(Guid id, CancellationToken cts = default);
}