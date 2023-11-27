namespace WebApi.Services;

using Microsoft.EntityFrameworkCore;
using WebApi.Dal;
using WebApi.Extensions;
using WebApi.Models;
using WebApi.Services.Contracts;
using OwnerDao = WebApi.Dal.Database.dbo.Owner;

internal class OwnerService : IOwnerService
{
    #region Fields

    private readonly IRepository myRepository;

    #endregion

    #region Constructors

    public OwnerService(IRepository repository)
    {
        this.myRepository = repository;
    }

    #endregion

    #region Methods

    public async Task<Owner> AddAsync(Owner owner)
    {
        var dao = await this.myRepository.AddAsync(
            new OwnerDao { Address = owner.Address!, Cellphone = owner.Cellphone!, Email = owner.Email, Name = owner.Name! });
        await this.myRepository.SaveChangesAsync();

        return OwnerService.Dao2Dto(dao);
    }

    private static Owner Dao2Dto(OwnerDao dao)
    {
        return new Owner
               {
                   Address = dao.Address,
                   Cellphone = dao.Cellphone,
                   Email = dao.Email,
                   Id = dao.Id,
                   Name = dao.Name
               };
    }

    public async Task<IPagedAsyncEnumerable<Owner>> GetAsync(int page, int pageSize)
    {
        var query = this.myRepository.Owners.OrderBy(p => p.Name);
        var total = await query.CountAsync();
        return query.Skip((page - 1) * pageSize).Take(pageSize).AsAsyncEnumerable().Select(OwnerService.Dao2Dto).ToPagedEnumerable(page, total);
    }

    public async Task<Owner?> GetById(int id)
    {
        return await this.myRepository.Owners.Where(p => p.Id == id).AsAsyncEnumerable().Select(OwnerService.Dao2Dto).FirstOrDefaultAsync();
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var owner = this.myRepository.Owners.FirstOrDefault(p => p.Id == id);

        if (owner == null)
        {
            return false;
        }

        var removed = this.myRepository.Remove(owner);
        await this.myRepository.SaveChangesAsync();
        return removed;
    }

    public async Task<Owner?> UpdateAsync(int id, Owner owner)
    {
        var dao = await this.myRepository.Owners.FirstOrDefaultAsync(p => p.Id == id);

        if (dao == null)
        {
            return default;
        }

        dao.Address = owner.Address ?? dao.Address;
        dao.Cellphone = owner.Cellphone ?? dao.Cellphone;
        dao.Email = owner.Email ?? dao.Email;
        dao.Name = owner.Name ?? dao.Name;

        await this.myRepository.SaveChangesAsync();
        return OwnerService.Dao2Dto(dao);
    }

    #endregion
}