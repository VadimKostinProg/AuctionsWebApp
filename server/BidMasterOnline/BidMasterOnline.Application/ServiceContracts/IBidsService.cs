using BidMasterOnline.Application.DTO;

namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for managing the auction bids.
    /// </summary>
    public interface IBidsService
    {
        /// <summary>
        /// Gets all bids of the specified auction.
        /// </summary>
        /// <param name="auctionId">Identifier of the auction to get bids.</param>
        /// <returns>Collection IEnumerable of the bids.</returns>
        Task<IEnumerable<BidDTO>> GetBidsForAuctionAsync(Guid auctionId);

        /// <summary>
        /// Gets all bids of the specified user.
        /// </summary>
        /// <param name="userId">Identifier of the user to get bids.</param>
        /// <param name="showOnlyWinning">Flag the determines whether to show only winning bids.</param>
        Task<IEnumerable<BidDTO>> GetBidsForUserAsync(Guid userId, bool showOnlyWinning = false);

        /// <summary>
        /// Sets new bid of the specified user to the specified auction.
        /// </summary>
        /// <param name="bid">Bid information to set.</param>
        Task SetBidAsync(SetBidDTO bid);
    }
}
