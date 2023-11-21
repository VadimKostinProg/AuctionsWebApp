using BidMasterOnline.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidMasterOnline.Application.DTO
{
    public class CreateUserDTO : RegisterDTO
    {
        [Required(ErrorMessage = "User role is required.")]
        public UserRole Role { get; set; }
    }
}
