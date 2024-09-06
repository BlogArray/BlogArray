using BlogArray.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class Comment : EntityBase
{
	[StringLength(120)]
	public string Author { get; set; } = default!;

	[StringLength(100)]
	public string AuthorEmail { get; set; } = default!;

	[StringLength(120)]
	public string? AuthorSite { get; set; } = default!;

	[StringLength(100)]
	public string? AuthorIP { get; set; } = default!;

	[StringLength(256)]
	public string? UserAgent { get; set; } = default!;

	public string RawContent { get; set; } = default!;

	public string ParsedContent { get; set; } = default!;

	public CommentStatus Status { get; set; }

	public int? ParentId { get; set; }

	public int PostId { get; set; }

	public virtual Comment Parent { get; set; } = default!;

	public virtual Post Post { get; set; } = default!;
}
