using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace auc_client.Models.Entities;

public class PartEntity
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Required, JsonPropertyName("name")]
    public string Name { get; set; }

    [Required, JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("marketprice")]
    public long? MarketPrice { get; set; }

    [Required, Range(0, 2, ErrorMessage = "Value must be b/w 0-2"), JsonPropertyName("parttype")]
    public int PartType { get; set; }

    [JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; set; }
}