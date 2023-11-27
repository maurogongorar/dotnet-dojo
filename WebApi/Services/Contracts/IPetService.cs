namespace WebApi.Services.Contracts;

using WebApi.Models;

public interface IPetService
{
    #region Methods

    Task<Pet> AddAsync(Pet pet);

    Task<Pet?> GetByNameAsync(string name);

    Task<IPagedAsyncEnumerable<Pet>> GetByOwnerAsync(int ownerId, int page, int pageSize);

    Task<IPagedAsyncEnumerable<Pet>> GetForAdoptingAsync(PetType? petType, int page, int pageSize);

    Task<bool> RemoveAsync(string name);

    Task<Pet?> UpdateAsync(string name, Pet pet);

    #endregion
}