namespace DataAccessLayer_AutoAuctioneer.Models;

public class ItemEntity
{
    public Guid? Id { get; set; }
    public Guid UserId { get; set; }
    public int Type { get; set; }
}