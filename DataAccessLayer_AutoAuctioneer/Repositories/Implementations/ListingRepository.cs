using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class ListingRepository : BaseRepository, IListingRepository
{
    private readonly IConfiguration _config;

    public ListingRepository(IConfiguration config) : base(config)
    {
        _config = config;
    }

    public async Task<List<Listing>?> GetAllListings()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Listing>> GetOwnedListings(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<Listing> GetListingById(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PostListing(Listing listing)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteListing(Listing listing)
    {
        throw new NotImplementedException();
    }
}