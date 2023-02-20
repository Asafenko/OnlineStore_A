namespace OnlineStore.Data.Repositories.Cart_Repo;

public interface ICartRepository
{
   
    Task<Cart> GetById(Guid id, CancellationToken cts = default);
    Task<IReadOnlyList<Cart>> GetAll(CancellationToken cts = default);
    Task Add(Cart entity, CancellationToken cts = default);
    Task Update(Cart entity,CancellationToken cts = default);
    Task DeleteById(Guid id, CancellationToken cts = default);
    Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken ctsToken = default);
}