using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface IListingRepository
    {
        Task<bool> DeleteListing(Guid id);
        Task<List<Listing>?> GetAllListings();
        Task<Listing?> GetListingById(Guid guid);
        Task<List<Listing>?> GetOwnedListings(Guid guid);
        Task<bool> PostListing(Listing listing);
    }
}