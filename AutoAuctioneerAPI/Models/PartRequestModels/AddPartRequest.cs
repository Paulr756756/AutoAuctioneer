namespace API_AutoAuctioneer.Models.PartRequestModels;

public class AddPartRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? MarketPrice { get; set; }
    public Guid UserId { get; set; }
    public int PartType { get; set; }

    //Engine
    public string? EngineType { get; set; }
    public double? Displacement { get; set; }
    public int? Horsepower { get; set; }
    public int? Torque { get; set; }

    //CustomizationPart
    public string? Category { get; set; }
    public string? Manufacturer { get; set; }

    //IndividualCarPart
    public string? CarMake { get; set; }
    public string? CarModel { get; set; }
    public string? Year { get; set; }
}