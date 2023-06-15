using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary_BidStamp
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options): base(options)
        {
                
        }

        //This is a property that will act as a table
        public DbSet<Stamp> Stamps { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Stamps)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder
                .Entity<User>()
                .HasMany(u=>u.Listings)
                .WithOne(l=>l.User)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder
                .Entity<Stamp>()
                .HasOne(s=>s.Listing)
                .WithOne(l=>l.Stamp)
                .OnDelete(DeleteBehavior.ClientCascade);

        }

    }
}
