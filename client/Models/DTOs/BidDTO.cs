using Client.Models.Entities;

namespace Client.Models.DTOs;

public class BidDTO
{
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public long BidAmount { get; set; }

    public static explicit operator BidDTO?(BidEntity? entity)
    {
        return entity == null
            ? null
            : new BidDTO
            {
                Id = entity.Id,
                ListingId = entity.ListingId,
                UserId = entity.UserId,
                BidAmount = entity.BidAmount
            };
    }
}