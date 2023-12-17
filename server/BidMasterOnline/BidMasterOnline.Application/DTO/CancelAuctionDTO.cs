using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for canceling the auction. (REQUEST)
    /// </summary>
    public class CancelAuctionDTO
    {
        [Required]
        public Guid AuctionId { get; set; }

        [Required]
        public string CancelingReason { get; set; }
    }
}
