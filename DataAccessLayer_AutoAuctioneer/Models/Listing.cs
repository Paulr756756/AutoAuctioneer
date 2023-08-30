using DataAccessLibrary_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Models;

public class Listing
{
    public List<Bid>? Bids = new();
    public Guid ListingId { get; set; }

    public Guid UserId { get; set; }

    /*public Guid? StampId { get; set; }*/
    public User User { get; set; }
    /*public Stamp? Stamp { get; set; }*/


    public Guid? CarId { get; set; }
    public Car? Car { get; set; }


    public Guid? CarPartId { get; set; }
    public Part? CarPart { get; set; }
}