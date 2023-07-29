namespace API_AutoAuctioneer.Models.BidRequestModels;

public class DeleteBidRequest
{
    public Guid UserId { get; set; }
    public Guid BidId { get; set; }
}