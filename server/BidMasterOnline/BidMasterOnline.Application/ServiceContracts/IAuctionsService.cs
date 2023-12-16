using BidMasterOnline.Application.DTO;

namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for managin tha auctions.
    /// </summary>
    public interface IAuctionsService
    {
        /// <summary>
        /// Gets list of auctions with applyed specifications.
        /// </summary>
        /// <param name="specifications">Specifications of sorting, pagination, filtering by category
        /// start price, current bid and status ty apply.</param>
        /// <returns>Auctions list with applyed specifications.</returns>
        Task<ListModel<AuctionDTO>> GetAuctionsListAsync(AuctionSpecificationsDTO specifications);

        /// <summary>
        /// Gets auctions information by its identifier.
        /// </summary>
        /// <param name="id">Identifier of auction to get information of.</param>
        /// <returns>Infromation of auction.</returns>
        Task<AuctionDTO> GetAuctionByIdAsync(Guid id);

        /// <summary>
        /// Gets auction detailed information by identifier.
        /// </summary>
        /// <param name="id">Identifier of auction to get information of.</param>
        /// <returns>Detailed infromation of auction.</returns>
        Task<AuctionDetailsDTO> GetAuctionDetailsByIdAsync(Guid id);

        /// <summary>
        /// Sets the score for the specified auction by user.
        /// </summary>
        /// <param name="request">Information of auction and score to set.</param>
        Task SetAuctionScoreAsync(SetAuctionScoreDTO request);

        /// <summary>
        /// Published auction for futher verification.
        /// </summary>
        /// <param name="request">Object with publishing auctions infromation.</param>
        Task PublishAuctionAsync(PublishAuctionDTO request);

        /// <summary>
        /// Confirms payment for auction lot by user.
        /// </summary>
        /// <param name="auctionId">Identifier of auction to confirm payment for.</param>
        Task ConfirmPaymentForAuctionAsync(Guid auctionId);

        /// <summary>
        /// Cancels active auction.
        /// </summary>
        /// <param name="auctionId">Identifier of auction to cancel.</param>
        Task CancelAuctionAsync(Guid auctionId);

        /// <summary>
        /// Recovers canceled auction.
        /// </summary>
        /// <param name="auctionId">Identifier of auction to cancel.</param>
        /// <returns></returns>
        Task RecoverAuctionAsync(Guid auctionId);
    }
}
