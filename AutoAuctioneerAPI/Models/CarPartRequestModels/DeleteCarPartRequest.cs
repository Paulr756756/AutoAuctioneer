namespace API_AutoAuctioneer.Models.CarPartRequestModels;

public class DeleteCarPartRequest
{
    public Guid UserId { get; set; }
    public Guid CarPartId { get; set; }
}