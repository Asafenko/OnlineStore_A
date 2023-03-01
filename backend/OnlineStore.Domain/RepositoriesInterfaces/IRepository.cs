namespace OnlineStore.Domain.RepositoriesInterfaces;

public interface IRepository<TEntity>
{
    Task<TEntity> GetById(Guid Id,CancellationToken ctsToken = default);
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken ctsToken = default);
    Task Add(TEntity entity,CancellationToken ctsToken = default);
    Task Update(TEntity entity,CancellationToken ctsToken = default);
    Task<TEntity> DeleteById(Guid id, CancellationToken ctsToken = default);
    Task Delete(CancellationToken ctsToken = default);
}