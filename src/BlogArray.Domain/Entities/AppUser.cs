using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class AppUser : IdentityUser<int>
{
	public string? DisplayName { get; set; }

	[MaxLength(512)]
	public string? Bio { get; set; }

	public DateTime CreatedOn { get; set; }

	public int? CreatedUserId { get; set; }

	public DateTime UpdatedOn { get; set; }

	public int? UpdatedUserId { get; set; }

	public virtual AppUser? CreatedUser { get; set; } = default!;

	public virtual AppUser? UpdatedUser { get; set; } = default!;
}
