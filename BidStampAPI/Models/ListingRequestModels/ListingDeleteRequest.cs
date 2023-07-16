using System.ComponentModel.DataAnnotations;

namespace API_BidStamp.Models.ListingRequestModels;

public class ListingDeleteRequest {
    [Required] public Guid ListingId { get; set; }

    [Required] public Guid UserId { get; set; }
}