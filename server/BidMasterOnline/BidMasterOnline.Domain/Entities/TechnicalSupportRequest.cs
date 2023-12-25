using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidMasterOnline.Domain.Entities
{
    public class TechnicalSupportRequest : EntityBase
    {
        private readonly ILazyLoader _loader;

        public TechnicalSupportRequest() { }

        public TechnicalSupportRequest(ILazyLoader loader)
        {
            _loader = loader;
        }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string RequestText { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public bool IsHandled { get; set; }

        #region NAVIGATION FIELDS
        private User _user;

        public User User
        {
            get => _loader.Load(this, ref _user);
            set => _user = value;
        }
        #endregion
    }
}
