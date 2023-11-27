namespace DotNetDojo.Controllers;

using DotNetDojo.Extensions;
using DotNetDojo.Models;
using DotNetDojo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("api/owners")]
[ApiController]
public class OwnersController : ControllerBase
{
    #region Fields

    private readonly IOwnerService myOwnerService;

    #endregion

    #region Constructors

    public OwnersController(IOwnerService ownerService)
    {
        this.myOwnerService = ownerService;
    }

    #endregion

    #region Methods

    [HttpPost]
    public async Task<IActionResult> AddOwner(Owner owner)
    {
        var newOwner = await this.myOwnerService.AddAsync(owner);
        return this.CreatedAtAction(nameof(OwnersController.GetOwnerById), new { ownerId = newOwner.Id }, newOwner);
    }

    [HttpGet]
    public async Task<PagedCollection<Owner>> GetAllOwners(int page = 1, [FromQuery(Name = "page-size")] int pageSize = 10)
    {
        var owners = await this.myOwnerService.GetAsync(page, pageSize);
        return await owners.ToPagedCollectionAsync();
    }

    [HttpGet("{ownerId}")]
    public async Task<ActionResult<Owner>> GetOwnerById(int ownerId)
    {
        var owner = await this.myOwnerService.GetById(ownerId);
        return owner == null ? this.NotFound() : owner;
    }

    [HttpDelete("{ownerId}")]
    public async Task<IActionResult> RemoveOwner(int ownerId)
    {
        var removed = await this.myOwnerService.RemoveAsync(ownerId);
        return removed ? this.NoContent() : this.NotFound();
    }

    [HttpPut("{ownerId}")]
    public async Task<ActionResult<Owner>> UpdateOwner(int ownerId, Owner owner)
    {
        var updatedOwner = await this.myOwnerService.UpdateAsync(ownerId, owner);
        return updatedOwner == null ? this.NotFound() : updatedOwner;
    }

    #endregion
}