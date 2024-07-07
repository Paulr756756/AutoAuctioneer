namespace DataAccessLayer_AutoAuctioneer.Models;

public class PartEntity
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public int? Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public long? MarketPrice { get; set; }
    public int PartType { get; set; }
    public string? Manufacturer { get; set; }
}

public class Part_Engine : PartEntity
{
    public string EngineType { get; set; }
    public decimal? Displacement { get; set; }
    public int? Horsepower { get; set; }
    public int? torque { get; set; }
}

public class Part_Specific : PartEntity
{
    public string? CarMake { get; set; }
    public string? CarModel { get; set; }
    public DateOnly? Year { get; set; }
}