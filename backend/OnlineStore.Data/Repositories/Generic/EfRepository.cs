using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Data.Repositories.Generic;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext DbContext;
    
    //1 - asp net core automatically throw exception
    //2 - manual instantiation : maybe null NRE
    protected EfRepository(AppDbContext dbContext)
    {
        // Fail Fast
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public DbSet<TEntity> Entities => DbContext.Set<TEntity>();

    // Get By Id
    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cts = default)
    {
        var product = await Entities.FirstAsync(it => it.Id == id, cts);
        return product;
    }

    // Get All
    public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cts = default)
    {
        var products = await Entities.ToListAsync(cts);
        return products;
    }

    // Add Entity
    public virtual async Task Add(TEntity entity, CancellationToken cts = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await Entities.AddAsync(entity, cts);
        //await DbContext.SaveChangesAsync(cts);
    }


    // Update By Id
    public virtual async Task Update(TEntity entity,CancellationToken cts = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        DbContext.Entry(entity).State = EntityState.Modified;
       // await DbContext.SaveChangesAsync(cts);
    }
    
    // Delete By Id
    public virtual async Task DeleteById(Guid id, CancellationToken cts = default)
    {
        var delEntity = await Entities.FirstAsync(it => it.Id == id, cts);
        Entities.Remove(delEntity);
        //await DbContext.SaveChangesAsync(cts);
    }
}