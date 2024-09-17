using BlogArray.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class Storage : AuthorEntityBase
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public AssetType AssetType { get; set; }

    [StringLength(2048)]
    public string Slug { get; set; } = default!;

    [StringLength(256)]
    public string Name { get; set; } = default!;

    [StringLength(2048)]
    public string Path { get; set; } = default!;

    public long Length { get; set; }

    [StringLength(128)]
    public string ContentType { get; set; } = default!;
}
