namespace DotNetDojo.Dal.Database;

using DotNetDojo.Dal.Database.dbo;
using Microsoft.EntityFrameworkCore;

internal class PetShelterDbContext : DbContext
{
    #region Properties

    public DbSet<Owner> Owners { get; set; }

    public DbSet<Pet> Pets { get; set; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    public PetShelterDbContext(DbContextOptions<PetShelterDbContext> options)
#pragma warning restore CS8618
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pet>(
            entity =>
            {
                entity.HasOne(p => p.Owner).WithMany(p => p.Pets).HasForeignKey(p => p.OwnerId);
            });
    }

    #endregion
}