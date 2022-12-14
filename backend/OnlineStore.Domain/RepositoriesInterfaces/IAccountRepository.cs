using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoriesInterfaces;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default);
    Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default);
}