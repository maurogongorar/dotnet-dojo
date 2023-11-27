namespace DotNetDojo.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class Owner
{
    #region Properties

    [Required]
    [StringLength(1024)]
    public string? Address { get; set; }

    [Required]
    [RegularExpression("^3[0-9]{9}$")]
    public string? Cellphone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [ReadOnly(true)]
    public int? Id { get; set; }

    [Required]
    [StringLength(128)]
    public string? Name { get; set; }

    #endregion
}