using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.CarRequestModels;

public class UpdateCarRequest
{
    [Required]
    public Guid CarId { get; set; }
    
    //Basic Attributes
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string VIN { get; set; }
    public int Mileage { get; set; }
    public string Transmission { get; set; }

    // Exterior Attributes
    public string Color { get; set; }
    public string? BodyStyle { get; set; }
    public List<string>? ExteriorFeatures { get; set; }

    // Interior Attributes
    public string? InteriorColor { get; set; }
    public int SeatingCapacity { get; set; }
    public List<string>? InteriorFeatures { get; set; }

    // Engine and Performance Attributes
    public string? EngineType { get; set; }
    public double? EngineDisplacement { get; set; }
    public int? Horsepower { get; set; }
    public int? Torque { get; set; }
    public double? FuelEfficiency { get; set; }

    // Customizations and Modifications
    public List<string>? AftermarketUpgrades { get; set; }
    public string? WheelsAndTires { get; set; }
    public string? Suspension { get; set; }
    public List<string>? AudioAndEntertainment { get; set; }

    // History and Maintenance
    public int? NumberOfPreviousOwners { get; set; }
    public List<string>? OwnershipHistory { get; set; }
    public bool HasAccidentHistory { get; set; }
    public List<string>? ServiceRecords { get; set; }

    // Additional Features
    public List<string>? SafetyFeatures { get; set; }
    public List<string>? TechnologyFeatures { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    public List<string>? ImageUrls { get; set; }
}