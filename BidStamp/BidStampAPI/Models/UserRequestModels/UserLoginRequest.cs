using System.ComponentModel.DataAnnotations;

namespace API_BidStamp.Models.UserRequestModels
{
    public class UserLoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
