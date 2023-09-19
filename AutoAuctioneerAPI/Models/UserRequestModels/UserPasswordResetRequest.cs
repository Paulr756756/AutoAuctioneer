﻿using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.UserRequestModels;

public class UserPasswordResetRequest
{
    [Required] public string Token { get; set; } = string.Empty;

    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;

    [Required] [Compare("Password")] public string ConfirmPassword { get; set; } = string.Empty;
}