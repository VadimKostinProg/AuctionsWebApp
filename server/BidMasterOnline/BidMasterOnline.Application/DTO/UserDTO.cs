﻿namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for users. (RESPONSE)
    /// </summary>
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}
