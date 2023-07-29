﻿using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.UserRequestModels;

public class UserRegisterRequest
{
    [Required]
    [MinLength(5)]
    [MaxLength(15)]
    public string UserName { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;

    [Required] [Compare("Password")] public string ConfirmPassword { get; set; } = string.Empty;
}