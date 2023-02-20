using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Generic;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Data.Repositories.Cart_Repo;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken ctsToken = default)
    {
        var cart = await Entities.FirstOrDefaultAsync(accId=>accId.AccountId==accountId, ctsToken);
        return cart;
    }
    
}