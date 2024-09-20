using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.DTOs;

public class LoginRequest
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}

public class LoginResult
{
    public bool Success { get; set; }

    public required string Type { get; set; }

    public string? Message { get; set; }

    public TokenResponse? TokenData { get; set; }
}

public class TokenResponse
{
    public required string Token { get; set; }

    public required int ExpiresIn { get; set; }

    public required DateTime IssueInUtc { get; set; }
}
