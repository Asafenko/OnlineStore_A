using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Generic;
using OnlineStore.Domain;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Data.Repositories.Product_Repo;

public class ProductRepository : EfRepository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cts = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        var products = await Entities
            .Where(it => it.Name.Contains(name))
            .ToListAsync(cts);
        return products;
    }
    
    // Get All Product
    // public async Task<IReadOnlyList<Product>> GetProducts(CancellationToken cts = default)
    // {
    //     var products = await _dbContext.Products.ToListAsync(cts);
    //     return products;
    // }
    //
    //
    // // Get By ID Product
    // public async Task<Product> GetProduct(Guid id, CancellationToken cts = default)
    // {
    //     var product = await _dbContext.Products.FirstAsync(ti => ti.Id == id, cts);
    //     return product;
    // }
    //
    //
    // // Add Product
    // public async Task AddProduct(Product product, CancellationToken cts = default)
    // {
    //     await _dbContext.Products.AddAsync(product, cts);
    //     await _dbContext.SaveChangesAsync(cts);
    // }
    //
    //
    // // Update Product 
    // public async Task UpdateProduct(Product product, CancellationToken cts = default)
    // {
    //     _dbContext.Entry(product).State = EntityState.Modified;
    //     await _dbContext.SaveChangesAsync(cts);
    // }
    //
    //
    // // Delete Product By ID
    // public async Task DeleteProduct(Guid id, CancellationToken cts = default)
    // {
    //     var product = await _dbContext.Products.FirstAsync(it => it.Id == id, cts);
    //     _dbContext.Products.Remove(product);
    //     await _dbContext.SaveChangesAsync(cts);
    // }
    //
    //
    // Delete All
    // public async Task Delete(CancellationToken cts = default)
    // {
    //     await _dbContext.Products.ExecuteDeleteAsync(cts);
    //     await _dbContext.SaveChangesAsync(cts);
    // }
    
}