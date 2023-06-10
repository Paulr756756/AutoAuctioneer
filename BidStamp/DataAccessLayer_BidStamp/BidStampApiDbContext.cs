using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary_BidStamp
{
    public class BidStampApiDbContext : DbContext
    {
        public BidStampApiDbContext(DbContextOptions options): base(options)
        {
                
        }

        //This is a property that will act as a table
        public DbSet<Stamp> Stamps { get; set; }
    }
}
