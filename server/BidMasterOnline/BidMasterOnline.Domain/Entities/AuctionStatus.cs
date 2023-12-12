﻿using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Domain.Entities
{
    public class AuctionStatus : EntityBase
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; } = null!;
    }
}
