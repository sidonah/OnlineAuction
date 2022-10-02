using Microsoft.EntityFrameworkCore;

namespace Auction.Models
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {

        }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> purchases { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
    }
}
