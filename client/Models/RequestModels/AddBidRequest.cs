
using System.ComponentModel.DataAnnotations;

namespace auc_client.Models; 

public class AddBidRequest {
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid ListingId { get; set; }
    
    [Required, DataType(DataType.Currency)]
    public long BidAmount { get; set; }
    
    public DateTime BidTime { get; set; }
}