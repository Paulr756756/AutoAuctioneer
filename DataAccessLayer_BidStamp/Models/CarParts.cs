using DataAccessLibrary_BidStamp.Models;

namespace DataAccessLayer_BidStamp.Models; 

public class CarPart {
    public Guid CarpartId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? MarketPrice { get; set; }
    public Listing? Listing { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}

public class Engine : CarPart {
    public string EngineType { get; set; }
    public double Displacement { get; set; }
    public int Horsepower { get; set; }
    public int Torque { get; set; }
}

public class CustomizationPart : CarPart {
    public string Category { get; set; }
    public string Manufacturer { get; set; }
}

public class IndividualCarPartBidModel : CarPart {
    public string CarMake { get; set; }
    public string CarModel { get; set; }
    public string Year { get; set; }
}

