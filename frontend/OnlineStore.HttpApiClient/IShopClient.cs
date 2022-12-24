using OnlineStore.Domain.Entities;
using OnlineStore.HttpModels.Requests;
using OnlineStore.HttpModels.Responses;

namespace OnlineStore.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>> GetProducts(CancellationToken cts = default);
    Task<Product> GetProduct(Guid id, CancellationToken cts = default);
    Task AddProduct(Product product, CancellationToken cts = default);
    Task UpdateProduct(Guid id, Product product, CancellationToken cts = default);
    Task DeleteById(Guid id, CancellationToken cts = default);
    Task Registration(RegisterRequest request, CancellationToken cts = default);
    Task<LogInResponse> Login(string email, string password, CancellationToken cts = default);


    //Task Delete(CancellationToken cts= default);
}