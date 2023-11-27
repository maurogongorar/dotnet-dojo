namespace WebApi.Dal;

using Microsoft.EntityFrameworkCore;
using WebApi.Dal.Database;
using WebApi.Dal.Database.dbo;

internal class Repository : IRepository
{
    #region Fields

    private readonly PetShelterDbContext myDbContext;

    #endregion

    #region Properties

    public IQueryable<Owner> Owners => this.myDbContext.Owners;

    public IQueryable<Pet> Pets => this.myDbContext.Pets;

    #endregion

    #region Constructors

    public Repository(PetShelterDbContext dbContext)
    {
        this.myDbContext = dbContext;
    }

    #endregion

    #region Methods

    public async ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return (await this.myDbContext.AddAsync(entity, cancellationToken)).Entity;
    }

    public bool Remove<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return this.myDbContext.Remove(entity).State == EntityState.Deleted;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return this.myDbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion
}