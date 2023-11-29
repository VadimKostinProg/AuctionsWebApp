using BidMasterOnline.Application.DTO;

namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for managing comments.
    /// </summary>
    public interface ICommentsService
    {
        /// <summary>
        /// Gets all comments for the specified auction.
        /// </summary>
        /// <param name="id">Identifier of the auction to get comments.</param>
        /// <returns>Collection IEnumerable of comments for the specified auction.</returns>
        Task<IEnumerable<CommentDTO>> GetCommentsByAuctionId(Guid id);

        /// <summary>
        /// Gets comment by it`s identifier.
        /// </summary>
        /// <param name="id">Identifier of comment to get.</param>
        /// <returns>Comment with passed identifier.</returns>
        Task<CommentDTO> GetCommentById(Guid id);

        /// <summary>
        /// Sets new comment of user for auction.
        /// </summary>
        /// <param name="comment">Comment to set.</param>
        Task SetNewComment(SetCommentDTO comment);

        /// <summary>
        /// Deletes comment with passed identifier.
        /// </summary>
        /// <param name="id">Identifier of comment to delete.</param>
        Task DeleteComment(Guid id);
    }
}
