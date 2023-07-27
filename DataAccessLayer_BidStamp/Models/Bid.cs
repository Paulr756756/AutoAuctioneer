using DataAccessLayer_BidStamp.Models;

namespace DataAccessLibrary_BidStamp.Models;
public class Bid {
    public Guid BidId { get; set; }
    public Guid ListingId { get; set; }
    public Listing Listing { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int BidAmount { get; set; }
    public DateTime BidTime { get; set; }
}