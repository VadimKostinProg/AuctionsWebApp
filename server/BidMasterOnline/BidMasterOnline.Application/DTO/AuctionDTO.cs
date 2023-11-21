using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidMasterOnline.Application.DTO
{
    public class AuctionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string AuctionersName { get; set; }
        public DateTime FinishDateAndTime { get; set; }
        public decimal StartPrice { get; set; }
        public decimal CurrentBid { get; set; }
        public string ImageUrl { get; set; }
    }
}
