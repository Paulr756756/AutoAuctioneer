using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.ListingRequestModels;

public class AddListingRequest
{
    [Required] public Guid UserId { get; set; }

    [Required] public Guid ItemId { get; set; }

    /*public Guid Id { get; set; }*/
}