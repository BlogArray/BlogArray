namespace BlogArray.Domain.Entities;

public class Statistic : KeyBase
{
    public string? UserAgent { get; set; } = default!;

    public DateTime ViewedOn { get; set; }

    public int? PostId { get; set; }

    public Post? Post { get; set; } = default!;
}
