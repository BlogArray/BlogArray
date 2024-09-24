using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface IUserRepository
{
    Task<PagedResult<BasicUserInfoRole>> GetPaginatedUsersAsync(int pageNumber, int pageSize, string? searchTerm);

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
    Task<UserInfo?> GetUserAsync(int userId);

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
    Task<ReturnResult<int>> CreateUserAsync(CreateUser user, int loggedInUser);

    /// <summary>
    /// Updates an existing user's information in the system.
    /// </summary>
    /// <param name="updateUser">The user information to update.</param>
    /// <param name="userIdToUpdate">The user id of user to update.</param>
    /// <param name="loggedInUser">The user id of current loggedin user.</param>
    /// <returns>
    /// A <see cref="ReturnResult{T}"/> object containing the result of the update operation.
    /// If successful, the <see cref="ReturnResult{T}.Status"/> will be true.
    /// </returns>
    /// <remarks>
    /// This method normalizes the user's email to lowercase, checks for email duplication,
    /// and allows for updating other details such as bio, display name, and password (if requested).
    /// </remarks>
    Task<ReturnResult<int>> EditUserAsync(EditUserInfo updateUser, int userIdToUpdate, int loggedInUser);

    /// <summary>
    /// Updates an existing user's information in the system except role.
    /// </summary>
    /// <param name="userProfile">The user information to update.</param>
    /// <param name="loggedInUser">The user id of current loggedin user.</param>
    /// <returns>
    /// A <see cref="ReturnResult{T}"/> object containing the result of the update operation.
    /// If successful, the <see cref="ReturnResult{T}.Status"/> will be true.
    /// </returns>
    /// <remarks>
    /// This method normalizes the user's email to lowercase, checks for email duplication,
    /// and allows for updating other details such as bio, display name, and password (if requested).
    /// </remarks>
    Task<ReturnResult<int>> UpdateProfileAsync(UserProfile userProfile, int loggedInUser);

    /// <summary>
    /// Deletes a user by their ID and optionally attributes their data to another user.
    /// </summary>
    /// <param name="id">The ID of the user to be deleted.</param>
    /// <param name="canAttributeTo">Specifies if the data of the deleted user can be attributed to another user.</param>
    /// <param name="attributeToUser">The ID of the user to whom the data should be attributed, if applicable.</param>
    /// <returns>A <see cref="ReturnResult{T}"/> containing the result of the operation, with the ID of the deleted user, or an error message if the operation fails.</returns>
    Task<ReturnResult<int>> DeleteUserAsync(int id, bool canAttributeTo, int attributeToUser);
}
