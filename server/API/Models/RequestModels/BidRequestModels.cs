using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.RequestModels;

public class AddBidRequest {
    [Required] public Guid ListingId { get; set; }
    [Required] public Guid UserId { get; set; }
    [Required] public long BidAmount { get; set; }
}
public class UpdateBidRequest {
    [Required] public Guid BidId { get; set; }
    [Required] public Guid UserId { get; set; }
    [Required] public long BidAmount { get; set; }
}
public class DeleteBidRequest {
    public Guid UserId { get; set; }
    public Guid BidId { get; set; }
}
public class BidsListingUserRequest {
    [Required] public Guid UserId { get; set; }
    [Required] public Guid ListingId { get; set; }
}