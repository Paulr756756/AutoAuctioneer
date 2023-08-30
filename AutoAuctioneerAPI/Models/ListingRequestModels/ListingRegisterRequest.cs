using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.ListingRequestModels;

public class ListingRegisterRequest
{
    [Required] public Guid UserId { get; set; }

    [Required] public Guid ItemId { get; set; }

    /*public Guid BidId { get; set; }*/
    [Required] public int Type { get; set; }
}