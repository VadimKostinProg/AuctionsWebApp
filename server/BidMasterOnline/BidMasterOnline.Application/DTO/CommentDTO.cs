using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for comments. (RESPONSE)
    /// </summary>
    public class CommentDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid AuctionId { get; set; }

        public string CommentText { get; set; }
    }
}
