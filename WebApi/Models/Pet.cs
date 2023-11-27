namespace DotNetDojo.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Pet
{
    #region Properties

    [StringLength(3, MinimumLength = 2)]
    [RegularExpression("^(yes)|(no)$")]
    public string IsAdopted { get; set; } = YesNo.No;

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    public string? OwnerName { get; set; }

    [ReadOnly(true)]
    public string? Tag { get; set; }

    [Required]
    [EnumDataType(typeof(PetType))]
    public PetType? Type { get; set; }

    #endregion
}