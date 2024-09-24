using BlogArray.Domain.Enums;

namespace BlogArray.Domain.DTOs;

public class BasicMediaInfo
{
    public int Id { get; set; }
}

public class MediaInfo : BasicMediaInfo
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public AssetType AssetType { get; set; }

    public string Slug { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string? Description { get; set; } = default!;

    public string Path { get; set; } = default!;

    public long Length { get; set; }

    public string ContentType { get; set; } = default!;

}
