using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.CarRequestModels;

public class DeleteCarRequest
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CarId { get; set; }
    
    
}