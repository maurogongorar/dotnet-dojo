namespace WebApi.Dal.Database.dbo;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Pet", Schema = "dbo")]
public class Pet
{
    #region Properties

    public bool IsAdopted { get; set; }

    [Key]
    [Required]
    [Column("PetName", TypeName = "varchar(50)")]
    public string Name { get; set; } = string.Empty;

    public Owner? Owner { get; set; }

    public int OwnerId { get; set; }

    [Required]
    [Column("PetTag", TypeName = "varchar(12)")]
    public string Tag { get; set; } = string.Empty;

    [Column("PetTypeId", TypeName = "smallint")]
    public short TypeId { get; set; }

    #endregion
}