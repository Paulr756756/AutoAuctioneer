namespace DataAccessLayer_AutoAuctioneer.Models;

public class CarPart
{
    public Guid CarpartId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? MarketPrice { get; set; }
    public Listing? Listing { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Engine? Engine { get; set; }
    public CustomizationPart? CustomizationPart { get; set; }

    public IndividualCarPart? IndividualCarPart { get; set; }

    //
    public int PartType { get; set; }
}

public class Engine : CarPart
{
    public Engine()
    {
        PartType = 1;
    }

    public string EngineType { get; set; }
    public double Displacement { get; set; }
    public int Horsepower { get; set; }
    public int Torque { get; set; }
    public CarPart? CarPart { get; set; }
}

public class CustomizationPart : CarPart
{
    public CustomizationPart()
    {
        PartType = 2;
    }

    public string Category { get; set; }
    public string Manufacturer { get; set; }
    public CarPart? CarPart { get; set; }
}

public class IndividualCarPart : CarPart
{
    public IndividualCarPart()
    {
        PartType = 3;
    }

    public string CarMake { get; set; }
    public string CarModel { get; set; }
    public string Year { get; set; }
    public CarPart? CarPart { get; set; }
}