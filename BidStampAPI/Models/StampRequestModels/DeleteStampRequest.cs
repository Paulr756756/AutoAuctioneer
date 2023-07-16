using System.ComponentModel.DataAnnotations;

namespace API_BidStamp.Models.StampRequestModels;

public class DeleteStampRequest {
    [Required] public Guid UserId { get; set; }

    [Required] public Guid StampId { get; set; }
}