using BidMasterOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BidMasterOnline.Infrastructure.DatabaseContext
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<AuctionFinishType> AuctionFinishTypes { get; set; }
        public virtual DbSet<AuctionStatus> AuctionStatuses { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<ComplaintType> ComplaintTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) { }

        public ApplicationContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
