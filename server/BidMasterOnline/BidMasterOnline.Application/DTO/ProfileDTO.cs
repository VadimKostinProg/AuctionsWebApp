using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for user profile. (RESPONSE)
    /// </summary>
    public class ProfileDTO : UserDTO
    {
        public int TotalAuctions { get; set; }
        public int TotalWins { get; set; }
    }
}
