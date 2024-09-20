using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogArray.Infrastructure.Repositories;

public class AccountRepository(AppDbContext db, IConfiguration Configuration) : IAccountRepository
{
    public async Task<SignInResultDTO> Authenticate(SignInDTO signIn)
    {
        AppUser? user = await db.AppUsers.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == signIn.Username || u.UserName == signIn.Username);

        // Validate user existence
        if (user == null)
        {
            return CreateInvalidSignInResult("Invalid username or password.");
        }

        // Validate password
        if (string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(signIn.Password, user.PasswordHash))
        {
            return CreateInvalidSignInResult("Invalid username or password.");
        }

        // Check if account is locked
        if (user.LockoutEnabled)
        {
            return CreateInvalidSignInResult("User account is locked for additional security.");
        }

        // Generate token for successful sign-in
        return new SignInResultDTO
        {
            Success = true,
            Type = "Token",
            Message = "Token generated successfully.",
            TokenData = GenerateTokenData(user)
        };
    }

    // Helper method to create standardized invalid sign-in results
    private static SignInResultDTO CreateInvalidSignInResult(string message) => new()
    {
        Success = false,
        Type = "Invalid",
        Message = message
    };

    public TokenData GenerateTokenData(AppUser appUser)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(Configuration["BlogArray:Jwt:Key"]));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        // Create claims
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.UserName),
            new Claim(ClaimTypes.GivenName, appUser.DisplayName ?? appUser.UserName),
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.Role, appUser.Role?.NormalizedName ?? "User") // Safe null handling
        ];

        // Token expiration in 7 days
        DateTime expire = DateTimeManager.Now().AddDays(7);
        JwtSecurityToken jwtToken = new(
            issuer: Configuration["BlogArray:Jwt:Issuer"],
            audience: Configuration["BlogArray:Jwt:Audience"],
            claims: claims,
            notBefore: DateTimeManager.Now(),
            expires: expire,
            signingCredentials: creds
        );

        // TokenData object creation
        return new TokenData
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            ExpiresInUtc = expire
        };
    }

}