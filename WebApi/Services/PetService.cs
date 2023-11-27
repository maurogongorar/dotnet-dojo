namespace WebApi.Services;

using DotNetDojo.Dal;
using DotNetDojo.Extensions;
using DotNetDojo.Models;
using DotNetDojo.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using PetDao = DotNetDojo.Dal.Database.dbo.Pet;

internal class PetService : IPetService
{
    #region Fields

    private readonly HttpClient myHttpClient;

    private readonly IRepository myRepository;

    #endregion

    #region Constructors

    public PetService(IRepository repository, HttpClient httpClient)
    {
        this.myRepository = repository;
        this.myHttpClient = httpClient;
    }

    #endregion

    #region Methods

    public async Task<Pet> AddAsync(Pet pet)
    {
        var owner = await this.myRepository.Owners.Select(p => new { p.Id, p.Name }).FirstOrDefaultAsync(p => p.Name == pet.OwnerName)
                    ?? new { Id = 1, Name = "Pet Shelter" };
        var response = await this.myHttpClient.PostAsync($"register/{pet.Type?.ToString() ?? "PET"}", null);
        var tag = await response.Content.ReadAsStringAsync();
        var dao = await this.myRepository.AddAsync(
            new PetDao
            {
                IsAdopted = pet.IsAdopted.Equals(YesNo.Yes),
                Name = pet.Name!,
                OwnerId = owner.Id,
                Tag = tag,
                TypeId = (short)pet.Type!
            });

        await this.myRepository.SaveChangesAsync();
        return new Pet
               {
                   Name = dao.Name,
                   IsAdopted = dao.IsAdopted ? YesNo.Yes : YesNo.No,
                   Type = (PetType)dao.TypeId,
                   Tag = dao.Tag,
                   OwnerName = owner.Name
               };
    }

    private static Pet Dao2Dto(PetDao dao)
    {
        return new Pet
               {
                   Name = dao.Name,
                   IsAdopted = dao.IsAdopted ? YesNo.Yes : YesNo.No,
                   Type = (PetType)dao.TypeId,
                   Tag = dao.Tag,
                   OwnerName = dao.Owner?.Name ?? "Pet Shelter"
               };
    }

    public async Task<Pet?> GetByNameAsync(string name)
    {
        return await this.myRepository.Pets.Include(p => p.Owner)
            .Where(p => p.Name == name)
            .AsAsyncEnumerable()
            .Select(PetService.Dao2Dto)
            .FirstOrDefaultAsync();
    }

    public async Task<IPagedAsyncEnumerable<Pet>> GetByOwnerAsync(int ownerId, int page, int pageSize)
    {
        var query = this.myRepository.Owners.Where(p => p.Id == ownerId).SelectMany(p => p.Pets).OrderBy(p => p.Name);
        var total = await query.CountAsync();
        return query.Skip((page - 1) * pageSize).Take(pageSize).AsAsyncEnumerable().Select(PetService.Dao2Dto).ToPagedEnumerable(page, total);
    }

    public async Task<IPagedAsyncEnumerable<Pet>> GetForAdoptingAsync(PetType? petType, int page, int pageSize)
    {
        var query = this.myRepository.Pets.Where(p => !p.IsAdopted && (!petType.HasValue || p.TypeId == (short)petType)).OrderBy(p => p.Name);
        var total = await query.CountAsync();
        return query.Skip((page - 1) * pageSize).Take(pageSize).AsAsyncEnumerable().Select(PetService.Dao2Dto).ToPagedEnumerable(page, total);
    }

    public async Task<bool> RemoveAsync(string name)
    {
        var pet = await this.myRepository.Pets.FirstOrDefaultAsync(p => p.Name == name);

        if (pet == null)
        {
            return false;
        }

        var removed = this.myRepository.Remove(pet);
        await this.myRepository.SaveChangesAsync();
        return removed;
    }

    public async Task<Pet?> UpdateAsync(string name, Pet pet)
    {
        var dao = await this.myRepository.Pets.FirstOrDefaultAsync(p => p.Name == name);
        var ownerId = await this.myRepository.Owners.Where(p => p.Name == pet.OwnerName).Select(p => (int?)p.Id).FirstOrDefaultAsync() ?? 1;

        if (dao == null)
        {
            return default;
        }

        dao.IsAdopted = pet.IsAdopted.Equals(YesNo.Yes);
        dao.TypeId = (short)pet.Type!;
        dao.OwnerId = ownerId;

        await this.myRepository.SaveChangesAsync();
        dao.Owner = ownerId == 1 ? null : new DotNetDojo.Dal.Database.dbo.Owner { Name = pet.OwnerName! };
        return PetService.Dao2Dto(dao);
    }

    #endregion
}