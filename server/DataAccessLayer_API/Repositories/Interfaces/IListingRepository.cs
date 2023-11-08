using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface IListingRepository
    {
        Task<bool> DeleteListing(Guid id);
        Task<List<ListingEntity>?> GetAllListings();
        Task<ListingEntity?> GetListingById(Guid guid);
        Task<List<ListingEntity>?> GetOwnedListings(Guid guid);
        Task<bool> PostListing(ListingEntity listing);
    }
}