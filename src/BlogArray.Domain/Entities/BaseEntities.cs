using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class KeyBase
{
	[Key]
	public int Id { get; set; }
}

public class AuthorEntityBase : KeyBase
{
	public DateTime CreatedOn { get; set; }

	public int CreatedUserId { get; set; }

	public virtual AppUser CreatedUser { get; set; } = default!;
}

public class EntityBase : AuthorEntityBase
{
	public DateTime UpdatedOn { get; set; }

	public int UpdatedUserId { get; set; }

	public virtual AppUser UpdatedUser { get; set; } = default!;
}
