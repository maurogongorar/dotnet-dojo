namespace WebApi.Services.Contracts;

using WebApi.Models;

public interface IOwnerService
{
    #region Methods

    Task<Owner> AddAsync(Owner owner);

    Task<IPagedAsyncEnumerable<Owner>> GetAsync(int page, int pageSize);

    Task<Owner?> GetById(int id);

    Task<bool> RemoveAsync(int id);

    Task<Owner?> UpdateAsync(int id, Owner owner);

    #endregion
}