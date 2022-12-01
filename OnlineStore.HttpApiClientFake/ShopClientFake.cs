using System.Collections.Concurrent;
using OnlineStore.HttpApiClient;
using OnlineStore.Models;

namespace OnlineStore.HttpApiClientFake;

public class ShopClientFake //: IShopClient
{
    // private readonly ConcurrentDictionary<int, Product> _product = new()
    // {
    //         [1]=new Product("asaf",654m,3)
    // };
    //
    // private readonly TimeSpan _delay;
    // public ShopClientFake(TimeSpan delay)
    // {
    //     _delay = delay;
    // }
    // public async Task<IReadOnlyList<Product>> GetProducts()
    // {
    //     await Task.Delay(_delay);
    //     return _product.Values.ToList().AsReadOnly();
    //     // return Task.CompletedTask;
    // }
    //
    // public async Task<Product> GetProduct(int id)
    // {
    //     await Task.Delay(_delay);
    //     return _product[id];
    //     //return Task.FromResult(_product[id]);
    //     //return Task.CompletedTask;
    // }
    //
    // public async Task AddProduct(Product product)
    // {
    //     await Task.Delay(_delay);
    //     _product.TryAdd(product.ID, product);
    // }
    //
    //
    // public async Task UpdateProduct(int id, Product product)
    // {
    //     await Task.Delay(_delay);
    //     _product.TryUpdate(product.ID, product, new Product());
    // }
    //
    // public async Task Delete()
    // {
    //     await Task.Delay(_delay);
    //     _product.Clear();
    // }
    //
    //
    //
    // public async Task DeleteProduct(int id, Product product)
    // {
    //     await Task.Delay(_delay);
    //     _product.TryRemove(id, out product);
    // }
}