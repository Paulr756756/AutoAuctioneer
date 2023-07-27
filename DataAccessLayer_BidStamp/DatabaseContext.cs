using DataAccessLayer_BidStamp.Models;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary_BidStamp;

public class DatabaseContext : DbContext {
    public DatabaseContext(DbContextOptions options) : base(options) { }

    //This is a property that will act as a table
    public DbSet<Stamp> Stamps { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<User>()
            .HasMany(u => u.Stamps)
            .WithOne(s => s.User)
            .OnDelete(DeleteBehavior.ClientCascade);
        modelBuilder
            .Entity<User>()
            .HasMany(u => u.Listings)
            .WithOne(l => l.User)
            .OnDelete(DeleteBehavior.ClientCascade);
        modelBuilder
            .Entity<Stamp>()
            .HasOne(s => s.Listing)
            .WithOne(l => l.Stamp)
            .OnDelete(DeleteBehavior.ClientCascade);
        /*modelBuilder
            .Entity<Stamp>()
            .HasOne(s => s.User)
            .WithMany(u => u.Stamps)
            .OnDelete(DeleteBehavior.ClientCascade);
        modelBuilder
            .Entity<Listing>()
            .HasOne(u => u.User)
            .WithMany(l => l.Listings)
            .OnDelete(DeleteBehavior.ClientCascade);*/
        modelBuilder
            .Entity<Bid>()
            .HasOne(e => e.Listing)
            .WithMany(e => e.Bids)
            .OnDelete(DeleteBehavior.ClientCascade);
        modelBuilder
            .Entity<Bid>()
            .HasOne(e => e.User)
            .WithMany(e => e.Bids)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        
        //For Cars Dbset
        /*modelBuilder
            .Entity<User>()
            .HasMany(u => u.Cars)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        modelBuilder
            .Entity<CarModel>()
            .HasOne(c => c.Listings)
            .WithOne(l => l.Car)
            .OnDelete(DeleteBehavior.ClientCascade);*/
    }
}