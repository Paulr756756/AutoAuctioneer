using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.ListingRequestModels;

public class ListingDeleteRequest
{
    [Required] public Guid Id { get; set; }

    [Required] public Guid UserId { get; set; }
}