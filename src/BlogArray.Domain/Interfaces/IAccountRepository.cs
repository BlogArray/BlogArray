using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface IAccountRepository
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
    Task<LoginResult> Authenticate(LoginRequest loginRequest);

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
    Task<UserInfo?> GetUser(int userId);

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
    Task<ReturnResult<int>> CreateUser(CreateUser user, int loggedInUser);

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
    Task<ReturnResult<int>> EditUser(EditUserInfo updateUser, int loggedInUser);
}
