using Client.Models.Entities;

namespace Client.Models.DTOs;

public class PartDTO {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public long? MarketPrice { get; set; }
    public int PartType { get; set; }
    public string? Manufacturer { get; set; }

    public static explicit operator PartDTO?(PartEntity? entity) {
        return entity == null ? null : new PartDTO {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Category = entity.Category,
            MarketPrice = entity.MarketPrice,
            PartType = entity.PartType,
            Manufacturer = entity.Manufacturer
        };
    }

}