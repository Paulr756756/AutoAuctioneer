using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IListingRepository
{
    Task<List<Listing>> getAllListings();
    Task<Listing> getListingById(Guid guid);
    Task postListing(Listing listing);
    Task deleteListing(Listing listing);
}

public class ListingRepository : IListingRepository
{
    private readonly DatabaseContext _dbContext;

    public ListingRepository(DatabaseContext databaseContext)
    {
        _dbContext = databaseContext;
    }

    public async Task<List<Listing>> getAllListings()
    {
        var response = await _dbContext.Listings.ToListAsync();
        return response;
    }

    public async Task<Listing> getListingById(Guid guid)
    {
        return await _dbContext.Listings.FirstOrDefaultAsync(l => l.ListingId == guid);
    }

    public async Task postListing(Listing listing)
    {
        _dbContext.Add(listing);
        await _dbContext.SaveChangesAsync();
    }

    public async Task deleteListing(Listing listing)
    {
        _dbContext.Listings.Remove(listing);
        await _dbContext.SaveChangesAsync();
    }
}