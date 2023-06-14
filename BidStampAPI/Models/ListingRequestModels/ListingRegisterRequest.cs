using System.ComponentModel.DataAnnotations;

namespace API_BidStamp.Models.ListingRequestModels
{
    public class ListingRegisterRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid StampId { get; set; }
        public Guid BidId { get; set; }
    }
}
