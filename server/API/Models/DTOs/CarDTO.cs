using API.Models.RequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Mozilla;

namespace API.Models.DTOs;

public class CarDTO {
    public Guid? Id { get; set; }
    public int Type { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string? Year { get; set; }
    public string? VIN { get; set; }
    public string? Color { get; set; }
    public string? BodyType { get; set; }
    public string? FuelType { get; set; }
    public string? TransmissionType { get; set; }
    public string? EngineType { get; set; }
    public int? Horsepower { get; set; }
    public int? Torque { get; set; }
    public double? FuelEfficiency { get; set; }
    public double? Acceleration { get; set; }
    public int? TopSpeed { get; set; }
    public string[]? ImageUrls { get; set; }

    public static explicit operator CarDTO(CarEntity entity) {
        return new CarDTO {
            Id = entity.Id,
            Type = entity.Type,
            Make = entity.Make,
            Model = entity.Model,
            Year = entity.Year,
            VIN = entity.VIN,
            Color = entity.Color,
            BodyType = entity.BodyType,
            FuelType = entity.FuelType,
            TransmissionType = entity.TransmissionType,
            EngineType = entity.EngineType,
            Horsepower = entity.Horsepower,
            Torque = entity.Torque,
            Acceleration = entity.Acceleration,
            FuelEfficiency = entity.FuelEfficiency,
            TopSpeed = entity.TopSpeed,
            ImageUrls = entity.ImageUrls
        };
    }
    public static explicit operator CarDTO(AddCarRequest request) {
        return new CarDTO {
            Type = request.Type,
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            VIN = request.VIN,
            Color = request.Color,
            BodyType = request.BodyType,
            FuelType = request.FuelType,
            TransmissionType = request.TransmissionType,
            EngineType = request.EngineType,
            Acceleration = request.Acceleration,
            FuelEfficiency = request.FuelEfficiency,
            TopSpeed = request.TopSpeed,
            ImageUrls = request.ImageUrls,
            Horsepower = request.Horsepower,
            Torque = request.Torque
        };
    }
    
}