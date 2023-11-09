using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models.RequestModels; 

public class AddCarRequest {
    [JsonPropertyName("carid")] public Guid Id { get; set; }
    [JsonPropertyName("userid")] public Guid UserId { get; set; }
    [JsonPropertyName("type")] public int Type { get; set; }

//Basic Attributes
    [JsonPropertyName("make")]
    [Required]
    public string Make { get; set; }

    [JsonPropertyName("model")]
    [Required]
    public string Model { get; set; }

    [Required]
    [JsonPropertyName("year"), MaxLength(4, ErrorMessage = "Cannot be greater than 4"), MinLength(4, ErrorMessage = "Cannot be smaller than 4")]
    public string Year { get; set; }

    [JsonPropertyName("vin")]
    public string VIN { get; set; }

    [JsonPropertyName("color")]
    [Required]
    public string Color { get; set; }

    [JsonPropertyName("bodytype")]
    [Required]
    public string BodyType { get; set; }

    [JsonPropertyName("fueltype")]
    [Required]
    public string FuelType { get;set; }

    [JsonPropertyName("transmissiontype")]
    public string TransmissionType { get; set; }

    [JsonPropertyName("engineType")]
    public string? EngineType { get; set; }

    [JsonPropertyName("horsepower")]
    public int? Horsepower { get; set; }
    [JsonPropertyName("torque")] public int? Torque { get; set; }
    [JsonPropertyName("fuelefficiency")] public double? FuelEfficiency { get; set; }
    [JsonPropertyName("acceleration")] public double? Acceleration { get; set; }
    [JsonPropertyName("topspeed")] public int? TopSpeed { get; set; }
    [JsonPropertyName("imageUrls")] public List<string>? ImageUrls { get; set; } = new();
}