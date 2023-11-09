using Client.Models.Entities;

namespace Client.Models.DTOs;
public class UserDto {
    public Guid? Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public static explicit operator UserDto(UserEntity user) {
        return new UserDto { 
            UserName = user.UserName,
            Email = user.Email,
            Address = user.Address,
            DateOfBirth = user.DateOfBirth,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
        };
    }

}
