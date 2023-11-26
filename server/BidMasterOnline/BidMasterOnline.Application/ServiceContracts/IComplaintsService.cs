using BidMasterOnline.Application.DTO;

namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for managing the complaints.
    /// </summary>
    public interface IComplaintsService
    {
        /// <summary>
        /// Gets the list of complaints for users.
        /// </summary>
        /// <returns>Collection IEnumerable of complaints.</returns>
        Task<IEnumerable<ComplaintDTO>> GetComplaintsForUsersAsync();

        /// <summary>
        /// Gets the list of complaints for auctions.
        /// </summary>
        /// <returns>Collection IEnumerable of complaints.</returns>
        Task<IEnumerable<ComplaintDTO>> GetComplaintsForAuctionsAsync();

        /// <summary>
        /// Set the status to the complaint as handled.
        /// </summary>
        /// <param name="id">Identifier of complaint to handle.</param>
        Task HandleComplaintAsync(Guid id);
    }
}
