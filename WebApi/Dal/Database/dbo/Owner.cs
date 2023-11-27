namespace DotNetDojo.Dal.Database.dbo;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PetOwner", Schema = "dbo")]
public class Owner
{
    #region Properties

    [Required]
    [Column("OwnerAddress", TypeName = "varchar(1024)")]
    public string Address { get; set; } = string.Empty;

    [Required]
    [Column("OwnerCellphone", TypeName = "varchar(10)")]
    public string Cellphone { get; set; } = string.Empty;

    [Column("OwnerEmail", TypeName = "varchar(512)")]
    public string? Email { get; set; }

    [Key]
    [Required]
    [Column("OwnerId")]
    public int Id { get; set; }

    [Required]
    [Column("OwnerName", TypeName = "varchar(128)")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

    #endregion
}