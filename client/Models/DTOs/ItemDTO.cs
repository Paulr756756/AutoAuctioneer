using auc_client.Models.Entities;

namespace auc_client.Models.DTOs;

public class ItemDTO {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Type { get; set; }

    public static explicit operator ItemDTO?(ItemEntity? entity) {
        return entity == null ? null : new ItemDTO { 
            Id = entity.Id,
            UserId = entity.UserId,
            Type = entity.Type
        };
    }
}
