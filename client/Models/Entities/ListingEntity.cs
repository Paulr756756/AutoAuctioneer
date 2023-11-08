using System.Text.Json.Serialization;

namespace auc_client.Models.Entities;

public class ListingEntity
{

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }

    [JsonPropertyName("itemId")]
    public Guid ItemId { get; set; }
}