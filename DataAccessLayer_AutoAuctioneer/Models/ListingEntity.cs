using DataAccessLibrary_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Models;

public class ListingEntity
{
    /*public List<BidEntity>? Bids = new();*/
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ItemId { get; set; }
}