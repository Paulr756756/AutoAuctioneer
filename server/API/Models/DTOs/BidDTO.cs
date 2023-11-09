using API.Models.RequestModels;
using DataAccessLayer_API.Models;

namespace API.Models.DTOs;
public class BidDTO {
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public long BidAmount { get; set; }
    public DateTime? BidTime { get; set; }

    public static explicit operator BidDTO?(BidEntity? entity) {
        return entity == null?null:new BidDTO {
            Id = entity.Id,
            ListingId = entity.ListingId,
            UserId = entity.UserId,
            BidAmount = entity.BidAmount,
            BidTime = entity.BidTime
        };
    }
    public static explicit operator BidDTO(AddBidRequest request) {
        return new BidDTO { 
            UserId = request.UserId,
            BidAmount = request.BidAmount,
            ListingId = request.ListingId
        };
    }
    public static implicit operator BidEntity(BidDTO dto) {
        return new BidEntity {
            Id = dto.Id,
            ListingId = dto.ListingId,
            UserId = dto.UserId,
            BidAmount = dto.BidAmount,
            BidTime = dto.BidTime
        };
    }
}