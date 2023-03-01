using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoriesInterfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetByName(string name, CancellationToken ctsToken = default);
    Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cancellationToken = default);
}