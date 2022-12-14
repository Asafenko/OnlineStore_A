using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Generic;
using OnlineStore.Domain;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Data.Repositories.Account_Repo;

public class AccountRepository : EfRepository<Account>,IAccountRepository
{
    public AccountRepository (AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
         //IReadonlulist       // await Entities.Where(em => em.Email.Contains(email)).ToListAsync(cancellationToken);
        var emailProduct = await Entities.FirstAsync(el => el.Email == email,cancellationToken);
        return emailProduct;
        
    }

    public async Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        var emailEx =  await Entities.FirstOrDefaultAsync(el=>el.Email == email,cancellationToken);
        return emailEx;
    }
}