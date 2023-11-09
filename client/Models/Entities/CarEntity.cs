using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models.Entities;

public class CarEntity
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    //Basic Attributes
    [Required, JsonPropertyName("make")]
    public string Make { get; set; }

    [Required, JsonPropertyName("model")]
    public string Model { get; set; }

    [Required, JsonPropertyName("year")]
    public string Year { get; set; }

    [JsonPropertyName("vin")]
    public string VIN { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("bodytype")]
    public string BodyType { get; set; }

    [Required, JsonPropertyName("fueltype")]
    public string FuelType { get; set; }

    [JsonPropertyName("transmissiontype")]
    public string TransmissionType { get; set; }

    [JsonPropertyName("engineType")]
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
}