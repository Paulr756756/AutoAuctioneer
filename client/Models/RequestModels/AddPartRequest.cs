using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace auc_client.Models.RequestModels; 

public class AddPartRequest {
    [JsonPropertyName("userId")] public Guid UserId { get; set; }
    
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [Required, JsonPropertyName("name")] public string Name { get; set; }

    [Required,JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("category")] public string? Category { get; set; }

    [JsonPropertyName("marketPrice")] public long? MarketPrice { get; set; }

    [Required,Range(0,2, ErrorMessage ="Value must be b/w 0-2"),JsonPropertyName("partType")]
    public int PartType { get; set; }

    [JsonPropertyName("manufacturer")] public string? Manufacturer { get; set; }
}