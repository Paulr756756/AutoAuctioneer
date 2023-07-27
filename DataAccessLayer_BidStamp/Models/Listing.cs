using DataAccessLibrary_BidStamp.Models;

namespace DataAccessLayer_BidStamp.Models;
public class Listing {
    public List<Bid>? Bids = new();
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public Guid StampId { get; set; }
    public User User { get; set; }
    public Stamp Stamp { get; set; }
    public Guid? BidId { get; set; }
}
