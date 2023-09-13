using DataAccessLibrary_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Models;

public class Listing
{
    /*public List<Bid>? Bids = new();*/
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ItemId { get; set; }
}