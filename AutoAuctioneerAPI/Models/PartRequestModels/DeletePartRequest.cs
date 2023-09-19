namespace API_AutoAuctioneer.Models.PartRequestModels;

public class DeletePartRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}