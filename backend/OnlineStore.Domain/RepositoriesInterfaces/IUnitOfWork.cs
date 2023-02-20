using OnlineStore.Data.Repositories.Cart_Repo;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Data.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IAccountRepository AccountRepository { get; }
    IProductRepository ProductRepository { get; }
    ICartRepository CartRepository { get; }
    ValueTask CommitAsync(CancellationToken cancellationToken = default);
    void Dispose();
    ValueTask DisposeAsync();
}