using System.Text.Json.Serialization;

namespace auc_client.Models.Entities;

public class ItemEntity
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("userid")]
    public Guid UserId { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }//if 0 then it's a car, else its a part
}