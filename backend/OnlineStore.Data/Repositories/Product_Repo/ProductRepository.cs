using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Generic;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Data.Repositories.Product_Repo;

public class ProductRepository : EfRepository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Product> GetByName(string name, CancellationToken ctsToken = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        var product =await Entities.FirstAsync(it => it.Name == name, ctsToken);
        return product;
    }
    
    public async Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cts = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        var products = await Entities
            .Where(it => it.Name.Contains(name))
            .ToListAsync(cts);
        return products;
    }
    
}