using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.DTOs;

public class SignIn
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}

public class SignInResult
{
    public bool Success { get; set; }

    public required string Type { get; set; }

    public string? Message { get; set; }

    public TokenData? TokenData { get; set; }
}

public class TokenData
{
    public required string Token { get; set; }

    public required DateTime ExpiresInUtc { get; set; }
}