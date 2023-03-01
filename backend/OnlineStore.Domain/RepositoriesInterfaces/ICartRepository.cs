using OnlineStore.Data;

namespace OnlineStore.Domain.RepositoriesInterfaces;

public interface ICartRepository: IRepository<Cart>
{
   
    
    Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken ctsToken = default);
    Task<Cart?> FindCartByAccountId(Guid accountId, CancellationToken ctsToken = default);
}