using DataAccessLayer_BidStamp.Models;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary_BidStamp;

public class DatabaseContext : DbContext {
    public DatabaseContext(DbContextOptions options) : base(options) { }

    //This is a property that will act as a table
    /*public DbSet<Stamp> Stamps { get; set; }*/
    public DbSet<Bid> Bids { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarPart> CarParts{ get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<CustomizationPart> CustomizationParts { get; set; }
    public DbSet<IndividualCarPart> IndividualCarParts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        /*modelBuilder
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
            .OnDelete(DeleteBehavior.ClientCascade);*/
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
        
        
        //For Cars 
        modelBuilder
            .Entity<User>()
            .HasMany(u => u.Cars)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        modelBuilder
            .Entity<Car>()
            .HasOne(c => c.Listing)
            .WithOne(l => l.Car)
            .HasForeignKey<Listing>(l=>l.CarId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        // For  CarParts
        modelBuilder
            .Entity<User>()
            .HasMany(u => u.CarParts)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        modelBuilder
            .Entity<CarPart>()
            .HasOne(c => c.Listing)
            .WithOne(l => l.CarPart)
            .HasForeignKey<Listing>(l=>l.CarPartId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        // For Engine
        modelBuilder
            .Entity<CarPart>()
            .HasOne(u => u.Engine)
            .WithOne(c => c.CarPart)
            .HasForeignKey<Engine>(e=>e.CarpartId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        //For CustomizationPart
        modelBuilder
            .Entity<CarPart>()
            .HasOne(c => c.CustomizationPart)
            .WithOne(l => l.CarPart)
            .HasForeignKey<CustomizationPart>(c=> c.CarpartId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        //For IndividualCarPart
        modelBuilder
            .Entity<CarPart>()
            .HasOne(u => u.IndividualCarPart)
            .WithOne(c => c.CarPart)
            .HasForeignKey<IndividualCarPart>(c=> c.CarpartId)
            .OnDelete(DeleteBehavior.ClientCascade);
     }
}