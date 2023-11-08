namespace auc_client.Models.Entities;

public class BidEntity
{
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public long BidAmount { get; set; }
    public DateTime? BidTime { get; set; }
}