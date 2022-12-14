using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoriesInterfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cancellationToken = default);
}