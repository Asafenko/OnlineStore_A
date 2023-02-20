using OnlineStore.Data.Repositories.Account_Repo;
using OnlineStore.Data.Repositories.Cart_Repo;
using OnlineStore.Data.Repositories.Product_Repo;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Data.UnitOfWork;

public class UnitOfWorkEf : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    

    public UnitOfWorkEf( AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }


    private IAccountRepository? _accountRepository;
    public IAccountRepository AccountRepository
    {
        get { return _accountRepository ??= new AccountRepository(_dbContext); }
    }

    private ICartRepository? _cartRepository;
    public ICartRepository CartRepository
    {
        get { return _cartRepository ??= new CartRepository(_dbContext); }
    }
    
    private IProductRepository? _productRepository;
    public IProductRepository ProductRepository
    {
        get { return _productRepository ??= new ProductRepository(_dbContext); }
    }

    


    public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _dbContext.Dispose();
    
    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}