using DataAccessLibrary_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task<bool> DeleteBid(Guid id);
        Task<List<Bid>?> GetAllBids();
        Task<Bid?> GetBidById(Guid id);
        Task<List<Bid>> GetBidsPerListing(Guid guid);
        Task<bool> PostBid(Bid bid);
        Task<bool> UpdateBidAmt(Bid bid, Guid id);
    }
}