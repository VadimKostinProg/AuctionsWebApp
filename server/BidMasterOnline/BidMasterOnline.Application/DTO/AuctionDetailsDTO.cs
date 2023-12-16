﻿using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO with detailed information of auction. (RESPONSE)
    /// </summary>
    public class AuctionDetailsDTO : AuctionDTO
    {
        public string Category { get; set; }
        public string AuctionistName { get; set; }
        public DateTime StartDateAndTime { get; set; }
        public string LotDescription { get; set; }
        public double Score { get; set; }
        public string FinishTypeDescription { get; set; }
        public AuctionStatus Status { get; set; }
        public List<BidDTO> Bids { get; set; }
    }
}
