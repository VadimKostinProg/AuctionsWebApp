﻿using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO with detailed information of auction. (RESPONSE)
    /// </summary>
    public class AuctionDetailsDTO : AuctionDTO
    {
        public string StartDateAndTime { get; set; }
        public string LotDescription { get; set; }
        public double Score { get; set; }
        public string FinishTypeDescription { get; set; }
        public string Status { get; set; }
        public Guid? WinnersId { get; set; }
        public string? WinnersUsername { get; set; }
    }
}
