﻿using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Domain.Entities
{
    public class ComplaintStatus : EntityBase
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
    }
}
