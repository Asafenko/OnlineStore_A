using OnlineStore.Domain.Entities;
using OnlineStore.HttpModels.Requests;
using OnlineStore.HttpModels.Responses;

namespace OnlineStore.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>> GetProducts(CancellationToken ctsToken = default);
    Task<Product> GetProduct(Guid id, CancellationToken ctsToken = default);
    Task AddProduct(Product product, CancellationToken ctsToken = default);
    Task UpdateProduct(Guid id, Product product, CancellationToken ctsToken = default);
    Task DeleteById(Guid id, CancellationToken ctsToken = default);
    Task<RegisterResponse> Registration (RegisterRequest request, CancellationToken ctsToken = default);
    Task<LogInResponse> Login(LogInRequest request, CancellationToken ctsToken = default);
    Task<LogInResponse?> GetCurrent(CancellationToken ctsToken = default);
    void SetAuthorizationToken(string token,CancellationToken ctsToken = default);
    void ResetAuthorizationToken();
}