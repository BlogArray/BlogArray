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
    public async Task<LoginResult> Authenticate(LoginRequest loginRequest)
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
    public async Task<ReturnResult<int>> RegisterUser(RegisterRequest user)
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

    /// <summary>
    /// Retrieves user information by user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="UserInfo"/> object if the user exists, or <c>null</c> if the user is not found.
    /// </returns>
    /// <remarks>
    /// This method fetches user details from the system based on the provided user ID.
    /// If the user does not exist, the method returns <c>null</c>.
    /// </remarks>
    public async Task<UserInfo?> GetUser(int userId)
    {
        UserInfo? userInfo = await db.AppUsers.Select(u => new UserInfo
        {
            Id = u.Id,
            Email = u.Email,
            Username = u.Username,
            DisplayName = u.DisplayName,
            EmailConfirmed = u.EmailConfirmed,
            LockoutEnabled = u.LockoutEnabled,
            Bio = u.Bio,
            LockoutEnd = u.LockoutEnd,
            RoleId = u.RoleId,
            Role = u.Role.NormalizedName
        }).FirstOrDefaultAsync(u => u.Id == userId);

        return userInfo;
    }

    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user data required for creating the account.</param>
    /// <param name="loggedInUser">The user id of current loggedin user.</param>
    /// <returns>
    /// A <see cref="ReturnResult{T}"/> containing the result of the user creation.
    /// If successful, <see cref="ReturnResult{T}.Result"/> contains the ID of the newly created user.
    /// </returns>
    /// <remarks>
    /// This method normalizes the user's email and username to lowercase and checks if they already exist in the system.
    /// If either the username or email is already registered, the method returns an error result.
    /// </remarks>
    public async Task<ReturnResult<int>> CreateUser(CreateUser user, int loggedInUser)
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

        // Create a new AppUser object
        AppUser newUser = new()
        {
            Email = user.Email,
            Username = user.Username,
            RoleId = user.RoleId,
            Bio = user.Bio,
            DisplayName = user.DisplayName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
            AccessFailedCount = 0,
            CreatedOn = DateTimeManager.Now(),
            CreatedUserId = loggedInUser,
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

    /// <summary>
    /// Updates an existing user's information in the system.
    /// </summary>
    /// <param name="updateUser">The user information to update.</param>
    /// <param name="loggedInUser">The user id of current loggedin user.</param>
    /// <returns>
    /// A <see cref="ReturnResult{T}"/> object containing the result of the update operation.
    /// If successful, the <see cref="ReturnResult{T}.Status"/> will be true.
    /// </returns>
    /// <remarks>
    /// This method normalizes the user's email to lowercase, checks for email duplication,
    /// and allows for updating other details such as bio, display name, and password (if requested).
    /// </remarks>
    public async Task<ReturnResult<int>> EditUser(EditUserInfo updateUser, int loggedInUser)
    {
        // Normalize email to lowercase
        updateUser.Email = updateUser.Email.ToLowerInvariant();

        /// <summary>
        /// Check if the email already exists for another user.
        /// </summary>
        if (await db.AppUsers.AnyAsync(a => a.Email == updateUser.Email && a.Id != updateUser.Id))
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = "User.EmailExists",
                Message = $"The email '{updateUser.Email}' is already registered with another account."
            };
        }

        // Retrieve the user by username
        AppUser? user = await db.AppUsers.FirstOrDefaultAsync(a => a.Username == updateUser.Username);

        /// <summary>
        /// If user is not found, return a not found error.
        /// </summary>
        if (user == null)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status404NotFound,
                Title = "User.NotFound",
                Message = $"The user with email '{updateUser.Email}' could not be found in the system."
            };
        }

        // Update user details
        user.Email = updateUser.Email;
        user.RoleId = updateUser.RoleId;
        user.Bio = updateUser.Bio;
        user.DisplayName = updateUser.DisplayName;

        // If password change is requested, hash the new password
        if (updateUser.ChangePassword)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUser.Password);
        }

        // Set additional fields for the user
        user.EmailConfirmed = true;
        user.LockoutEnabled = false;
        user.TwoFactorEnabled = false;
        user.UpdatedOn = DateTimeManager.Now();
        user.UpdatedUserId = loggedInUser;

        // Save changes to the database
        await db.SaveChangesAsync();

        /// <summary>
        /// Return success result indicating the user was updated.
        /// </summary>
        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "User.Updated",
            Message = $"User '{updateUser.Username}' was successfully updated."
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