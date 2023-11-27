namespace DotNetDojo.Controllers;

using DotNetDojo.Extensions;
using DotNetDojo.Models;
using DotNetDojo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("api/pets")]
[ApiController]
public class PetsController : ControllerBase
{
    #region Fields

    private readonly IPetService myPetService;

    #endregion

    #region Constructors

    public PetsController(IPetService petService)
    {
        this.myPetService = petService;
    }

    #endregion

    #region Methods

    [HttpPost]
    public async Task<IActionResult> AddPet(Pet pet)
    {
        var newPet = await this.myPetService.AddAsync(pet);
        return this.CreatedAtAction(nameof(PetsController.GetPetByName), new { name = newPet.Name }, newPet);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Pet>> GetPetByName(string name)
    {
        var pet = await this.myPetService.GetByNameAsync(name);
        return pet == null ? this.NotFound() : pet;
    }

    [HttpGet("~/api/owners/{ownerId}/pets")]
    public async Task<PagedCollection<Pet>> GetPetsByOwner(int ownerId, int page = 1, [FromQuery(Name = "page-size")] int pageSize = 10)
    {
        var pets = await this.myPetService.GetByOwnerAsync(ownerId, page, pageSize);
        return await pets.ToPagedCollectionAsync();
    }

    [HttpGet("for-adopting")]
    public async Task<PagedCollection<Pet>> GetPetsForAdopting(PetType? petType, int page = 1, [FromQuery(Name = "page-size")] int pageSize = 10)
    {
        var pets = await this.myPetService.GetForAdoptingAsync(petType, page, pageSize);
        return await pets.ToPagedCollectionAsync();
    }

    [HttpDelete("{name}")]
    public async Task<ActionResult<Pet>> RemovePet(string name)
    {
        var removed = await this.myPetService.RemoveAsync(name);
        return removed ? this.NoContent() : this.NotFound();
    }

    [HttpPut("{name}")]
    public async Task<ActionResult<Pet>> UpdatePet(string name, Pet pet)
    {
        var updatedPet = await this.myPetService.UpdateAsync(name, pet);
        return updatedPet == null ? this.NotFound() : updatedPet;
    }

    #endregion
}