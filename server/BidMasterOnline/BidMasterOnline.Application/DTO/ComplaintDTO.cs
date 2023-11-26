using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for complaint (RESPONSE).
    /// </summary>
    public class ComplaintDTO
    {
        public Guid AccusingUserId { get; set; }
        public UserDTO AccusingUser { get; set; }
        public Guid AccusedUserId { get; set; }
        public UserDTO AccusedUser { get; set; }
        public Guid AuctionId { get; set; }
        public AuctionDTO Auction { get; set; }
        public string ComplaintType { get; set; }
        public DateTime DateAndTime { get; set; }
        public string ComplaintText { get; set; }
    }
}
