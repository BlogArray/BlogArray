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
    Task<ReturnResult<int>> RegisterUser(RegisterRequest user);

}
