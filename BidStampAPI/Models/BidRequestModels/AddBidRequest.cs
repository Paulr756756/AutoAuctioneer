using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.BidRequestModels;

public class AddBidRequest
{
    [Required] public Guid ListingId { get; set; }

    [Required] public Guid UserId { get; set; }

    [Required] public int BidAmount { get; set; }
    /*public DateTime BidTime { get; set; }*/
}