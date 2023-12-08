using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Domain.Entities
{
    public class AuctionImage : EntityBase
    {
        [Required]
        public Guid AuctionId { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
