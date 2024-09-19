using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class AppRole : KeyBase
{
    [Required]
    [MaxLength(128)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(128)]
    public required string NormalizedName { get; set; }
}
