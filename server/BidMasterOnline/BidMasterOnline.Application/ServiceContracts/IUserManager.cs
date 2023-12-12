using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for managing users.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Gets user list with applyed specifications.
        /// </summary>
        /// <param name="specifications">Specifications of sorting, pagination, filtering by user role and 
        /// status to apply.</param>
        /// <returns>Users list with applyed specifications.</returns>
        Task<ListModel<UserDTO>> GetUsersListAsync(UserSpecificationsDTO specifications);

        /// <summary>
        /// Gets users information by it`s identifier.
        /// </summary>
        /// <param name="id">Identifier of user to get information about.</param>
        /// <returns>Users information.</returns>
        Task<UserDTO> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Create new user of specified role.
        /// </summary>
        /// <param name="request">Information of user to create.</param>
        /// <param name="role">Role of user to create.</param>
        Task CreateUserAsync(CreateUserDTO request, UserRole role);

        /// <summary>
        /// Changes the password of the specified user.
        /// </summary>
        /// <param name="request">Object with password information.</param>
        Task ChangePasswordAsync(ChangePasswordDTO request);

        /// <summary>
        /// Blocks the specified user for the specified amount of days.
        /// </summary>
        /// <param name="userId">Identifier of user to block.</param>
        /// <param name="days">Amount of days to block.</param>
        Task BlockUserAsync(Guid userId, int? days);

        /// <summary>
        /// Unblocks the specified user.
        /// </summary>
        /// <param name="userId">Identiier of user to unblock.</param>
        Task UnblockUserAsync(Guid userId);

        /// <summary>
        /// Applyes soft deleting of the specified user.
        /// </summary>
        /// <param name="userId">Identifier of user to delete.</param>
        Task DeleteUserAsync(Guid userId);
    }
}
