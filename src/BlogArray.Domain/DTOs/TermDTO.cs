namespace BlogArray.Domain.DTOs;

public class BasicTermInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Slug { get; set; } = default!;
}

public class TermInfo : TermInfoDescription
{
    public int PostsCount { get; set; } = default!;
}

public class TermInfoDescription : BasicTermInfo
{
    public string? Description { get; set; } = default!;
}