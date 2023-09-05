using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_AutoAuctioneer.Models.CarRequestModels;

public class AddCarRequest
{
    public Guid? CarId { get; set; }

    [JsonPropertyName("userid")]
    [Required]
    public Guid UserId { get; set; }

    //Basic Attributes
    [Required]
    [JsonPropertyName("make")]
    public string Make { get; set; }

    [Required]
    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("year")]
    public int? Year { get; set; }

    [JsonPropertyName("vin")]
    public string? VIN { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("bodytype")]
    public string? BodyType { get; set; }

    [JsonPropertyName("fueltype")]
    public string? FuelType { get; set; }

    [JsonPropertyName("transmissiontype")]
    public string? TransmissionType { get; set; }

    [JsonPropertyName("enginetype")]
    public string? EngineType { get; set; }

    [JsonPropertyName("horsepower")]
    public int? Horsepower { get; set; }

    [JsonPropertyName("torque")]
    public int? Torque { get; set; }

    [JsonPropertyName("fuelefficiency")]
    public double? FuelEfficiency { get; set; }

    [JsonPropertyName("acceleration")]
    public double? Acceleration { get; set; }

    [JsonPropertyName("topspeed")]
    public int? TopSpeed { get; set; }

    [JsonPropertyName("imageurls")]
    public List<string>? ImageUrls { get; set; }




    /*
         [Required]
    public Guid UserId { get; set; }
    //Basic Attributes
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string VIN { get; set; }
    public int Mileage { get; set; }
    public string? TransmissionType { get; set; }
    public int? TopSpeed { get; set; }
    public double Acceleration { get; set; }

    // Exterior Attributes
    public string Color { get; set; }
    public string? BodyType { get; set; }
    public string? FuelType { get; set; }
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

    // Image URLs (to be stored in Firebase)
    public List<string>? ImageUrls { get; set; }

    */
}