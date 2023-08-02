namespace API_AutoAuctioneer.Models.CarPartRequestModels;

public class UpdateCarPartRequest
{
    public Guid CarpartId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? MarketPrice { get; set; }
    public Guid UserId { get; set; }
    public int PartType { get; set; }
}