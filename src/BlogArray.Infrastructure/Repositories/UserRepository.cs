using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
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

}