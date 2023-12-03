using BidMasterOnline.Application.DTO;

namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for generating authorization token for user.
    /// </summary>
    public interface IJWTService
    {
        /// <summary>
        /// Generates JSON web token for the specified user.
        /// </summary>
        /// <param name="user">User to generate JWT for.</param>
        /// <returns>Authorization token for the specified user.</returns>
        string GenerateJWT(UserDTO user);

        /// <summary>
        /// Gets authorized user authorized with help of JSON web token.
        /// </summary>
        /// <returns>Authorized user.</returns>
        Task<UserDTO> GetAuthorizedUserAsync();
    }
}
