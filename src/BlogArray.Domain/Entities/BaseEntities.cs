using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class KeyBase
{
    [Key]
    public int Id { get; set; }
}

public class EntityBase : KeyBase
{
    public DateTime CreatedOn { get; set; }

    public int CreatedUserId { get; set; }

    public DateTime UpdatedOn { get; set; }

    public int UpdatedUserId { get; set; }

    public virtual AppUser CreatedUser { get; set; } = default!;

    public virtual AppUser UpdatedUser { get; set; } = default!;
}