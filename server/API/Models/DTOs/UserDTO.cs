using DataAccessLayer_AutoAuctioneer.Models;

namespace API.Models.DTOs;
public class UserDTO {
    public Guid? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }

    public static explicit operator UserDTO?(UserEntity? entity) {
        return entity==null?null:new UserDTO {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Address = entity.Address,
            Phone = entity.Phone,
        };
    }
}
 
