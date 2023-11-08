using auc_client.Models.Entities;

namespace auc_client.Models.DTOs;

public class ListingDTO { 
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ItemId { get; set; }

    public static explicit operator ListingDTO?(ListingEntity? entity) {
        return entity == null ? null : new ListingDTO {
            Id = entity.Id,
            UserId = entity.UserId,
            ItemId = entity.ItemId
        };
    }

}
