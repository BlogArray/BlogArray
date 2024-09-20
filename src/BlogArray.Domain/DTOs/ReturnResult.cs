namespace BlogArray.Domain.DTOs;

public class ReturnResult<T> : ReturnResult
{
    public required T Result { get; set; }
}

public class ReturnResult
{
    public bool Status { get; set; } = false;
    public required string Message { get; set; }
}