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
    public async Task<LoginResult> Authenticate(LoginRequest loginRequest)
    {
        AppUser? user = await db.AppUsers.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == loginRequest.Username || u.UserName == loginRequest.Username);

        // Validate user existence
        if (user == null)
        {
            return CreateInvalidLoginResult("Invalid username or password.");
        }

        // Validate password
        if (string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return CreateInvalidLoginResult("Invalid username or password.");
        }

        // Check if account is locked
        if (user.LockoutEnabled)
        {
            return CreateInvalidLoginResult("User account is locked for additional security.");
        }

        // Generate token for successful sign-in
        return new LoginResult
        {
            Success = true,
            Type = "Token",
            Message = "Token generated successfully.",
            TokenData = GenerateTokenData(user)
        };
    }

    public async Task<UserInfo?> GetUser(int userId)
    {
        UserInfo? userInfo = await db.AppUsers.Select(u => new UserInfo
        {
            Id = u.Id,
            Email = u.Email,
            UserName = u.UserName,
            DisplayName = u.DisplayName,
            EmailConfirmed = u.EmailConfirmed,
            LockoutEnabled = u.LockoutEnabled,
            Bio = u.Bio,
            LockoutEnd = u.LockoutEnd,
            Role = u.Role.NormalizedName
        }).FirstOrDefaultAsync(u => u.Id == userId);

        return userInfo;
    }

    // Helper method to create standardized invalid sign-in results
    private static LoginResult CreateInvalidLoginResult(string message, string type = "Invalid") => new()
    {
        Success = false,
        Type = type,
        Message = message
    };

    private TokenResponse GenerateTokenData(AppUser appUser)
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
            new Claim(ClaimTypes.Role, appUser.Role.NormalizedName)
        ];

        // Token expiration in 7 days
        int vaildDays = 7;
        DateTime issueDate = DateTimeManager.Now();
        DateTime expire = issueDate.AddDays(vaildDays);

        JwtSecurityToken jwtToken = new(
            issuer: Configuration["BlogArray:Jwt:Issuer"],
            audience: Configuration["BlogArray:Jwt:Audience"],
            claims: claims,
            notBefore: issueDate,
            expires: expire,
            signingCredentials: creds
        );

        // TokenData object creation
        return new TokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            ExpiresIn = vaildDays * 24 * 60 * 60,
            IssueInUtc = issueDate
        };
    }

}