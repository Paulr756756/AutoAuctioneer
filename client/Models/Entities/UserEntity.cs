using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models.Entities;

public class UserEntity
{
    [JsonPropertyName("id")] public Guid? Id { get; set; }

    [JsonPropertyName("username")]
    [Required]
    [MaxLength(25, ErrorMessage = "Username cannot have more than 25 letters")]
    public string UserName { get; set; }

    [JsonPropertyName("email")]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [JsonPropertyName("address")]
    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }

    [JsonPropertyName("dateofbirth")]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [JsonPropertyName("firstname")]
    [MaxLength(15, ErrorMessage = "Firstname cannot have more than 15 letters")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastname")]
    [MaxLength(15, ErrorMessage = "Lastname cannot have more than 15 letters")]
    public string? LastName { get; set; }

    [JsonPropertyName("phoneno")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    [JsonPropertyName("registrationdate")]
    [Required]
    [DataType(DataType.PhoneNumber)]
    public DateTime? RegistrationDate { get; set; }
}