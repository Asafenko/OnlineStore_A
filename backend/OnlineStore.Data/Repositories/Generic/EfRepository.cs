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
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    protected DbSet<TEntity> Entities => DbContext.Set<TEntity>();

    // Get By Id
    public virtual async Task<TEntity> GetById(Guid id, CancellationToken ctsToken = default)
    {
        var entity = await Entities.FirstAsync(it => it.Id == id, ctsToken);
        return entity;
    }

    // Get All
    public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken ctsToken = default)
    {
        var entities = await Entities.ToListAsync(ctsToken);
        return entities;
    }
    

    // Add Entity
    public virtual async Task Add(TEntity entity, CancellationToken ctsToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await Entities.AddAsync(entity, ctsToken);
    }

    

    //Update By Id
    public virtual async Task Update(TEntity entity,CancellationToken ctsToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        DbContext.Entry(entity).State = EntityState.Modified;
        //return ValueTask.CompletedTask;
    }
    
    // Delete By Id
    public virtual async Task<TEntity?> DeleteById(Guid id, CancellationToken ctsToken = default)
    {
        var delEntity = await Entities.FirstAsync(it => it.Id == id, ctsToken);
        Entities.Remove(delEntity);
        return delEntity;
    }
    
    public virtual async Task Delete(CancellationToken ctsToken = default)
    {
        await Entities.ExecuteDeleteAsync(ctsToken);
    }
}