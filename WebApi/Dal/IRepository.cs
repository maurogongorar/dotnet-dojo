namespace DotNetDojo.Dal;

using DotNetDojo.Dal.Database.dbo;

public interface IRepository
{
    #region Properties

    IQueryable<Owner> Owners { get; }

    IQueryable<Pet> Pets { get; }

    #endregion

    #region Methods

    ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class;

    bool Remove<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion
}