using System.ComponentModel.DataAnnotations;

namespace API_BidStamp.Models.BidRequestModels
{
    public class UpdateBidRequest
    {
        [Required]
        public Guid BidId { get; set; }
        [Required] 
        public Guid UserId { get; set; }
        [Required] 
        public int BidAmount { get; set; }
    }
}
