using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogArray.Infrastructure.Repositories;

public class AccountRepository(AppDbContext db, IConfiguration Configuration) : IAccountRepository
{
    /// <summary>
    /// Authenticates a user based on the provided login request.
    /// </summary>
    /// <param name="loginRequest">The login credentials and details provided by the user.</param>
    /// <returns>
    /// A task that represents the asynchronous authentication operation. 
    /// The task result contains a <see cref="LoginResult"/> object that holds authentication status and any relevant user or token information.
    /// </returns>
    /// <remarks>
    /// This method validates the user's credentials and returns authentication information, such as JWT tokens or user details.
    /// If authentication fails, the returned <see cref="LoginResult"/> will indicate the failure reason.
    /// </remarks>
    public async Task<LoginResult> AuthenticateAsync(LoginRequest loginRequest)
    {
        AppUser? user = await db.AppUsers.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == loginRequest.Username || u.Username == loginRequest.Username);

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
            return CreateInvalidLoginResult("User account is locked for additional security.", "User.Lockedout");
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

    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user data required for creating the account.</param>
    /// <returns>
    /// A <see cref="ReturnResult{T}"/> containing the result of the user creation.
    /// If successful, <see cref="ReturnResult{T}.Result"/> contains the ID of the newly created user.
    /// </returns>
    /// <remarks>
    /// This method normalizes the user's email and username to lowercase and checks if they already exist in the system.
    /// If either the username or email is already registered, the method returns an error result.
    /// </remarks>
    public async Task<ReturnResult<int>> RegisterUserAsync(RegisterRequest user)
    {
        // Normalize email and username to lowercase
        user.Email = user.Email.ToLowerInvariant();
        user.Username = user.Username.ToLowerInvariant();

        // Check if username already exists
        if (await db.AppUsers.AnyAsync(a => a.Username == user.Username))
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = "User.UsernameExists",
                Message = $"The username '{user.Username}' is already registered."
            };
        }

        // Check if email already exists
        if (await db.AppUsers.AnyAsync(a => a.Email == user.Email))
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = "User.EmailExists",
                Message = $"The email '{user.Email}' is already associated with another account."
            };
        }

        // SECURITY: Need to set default role from app settings
        // Create a new AppUser object
        AppUser newUser = new()
        {
            Email = user.Email,
            Username = user.Username,
            DisplayName = user.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
            AccessFailedCount = 0,
            CreatedOn = DateTimeManager.Now(),
            EmailConfirmed = true,
            LockoutEnabled = false,
            TwoFactorEnabled = false
        };

        // Add the new user to the database
        await db.AppUsers.AddAsync(newUser);
        await db.SaveChangesAsync();

        // Return success result
        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "User.Created",
            Message = $"User '{user.Username}' was successfully created.",
            Result = newUser.Id
        };
    }

    // Helper method to create standardized invalid sign-in results
    private static LoginResult CreateInvalidLoginResult(string message, string type = "Invalid")
    {
        return new()
        {
            Success = false,
            Type = type,
            Message = message
        };
    }

    private TokenResponse GenerateTokenData(AppUser appUser)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(Configuration["BlogArray:Jwt:Key"]));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        // Create claims
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.Username),
            new Claim(ClaimTypes.GivenName, appUser.DisplayName ?? appUser.Username),
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