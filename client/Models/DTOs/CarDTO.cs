using auc_client.Models.Entities;

namespace auc_client.Models.DTOs;

public class CarDTO {
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string? Year { get; set; }
    public string? Color { get; set; }
    public string? BodyType { get; set; }
    public string? FuelType { get; set; }
    public string? EngineType { get; set; }
    public string? TransmissionType { get; set; }
    public int? Horsepower { get; set; }
    public int? TopSpeed { get; set; }
    public double? Acceleration { get; set; }
    public string? ImageUrl { get; set; }

    public static explicit operator CarDTO?(CarEntity? car) {
        return car == null ? null : new CarDTO {
            Make = car.Make,
            Model = car.Model,
            Year = car.Year,
            Color = car.Color,
            BodyType = car.BodyType,
            FuelType = car.FuelType,
            EngineType = car.EngineType,
            TransmissionType = car.TransmissionType,
            Horsepower = car.Horsepower,
            TopSpeed = car.TopSpeed,
            Acceleration = car.Acceleration,
            ImageUrl = car.ImageUrls?[0]
        };
    }
}
