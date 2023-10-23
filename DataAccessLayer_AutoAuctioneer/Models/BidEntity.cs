using DataAccessLayer_AutoAuctioneer.Models;
using System.Numerics;

namespace DataAccessLibrary_AutoAuctioneer.Models;

public class BidEntity
{
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public long BidAmount { get; set; }
    public DateTime? BidTime { get; set; }
}