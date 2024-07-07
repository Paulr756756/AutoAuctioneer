using System.ComponentModel.DataAnnotations;

namespace API.Models.RequestModels;

public class AddListingRequest
{
    [Required] public Guid UserId { get; set; }

    [Required] public Guid ItemId { get; set; }

    /*public Guid Id { get; set; }*/
}

public class DeleteListingRequest
{
    [Required] public Guid Id { get; set; }

    [Required] public Guid UserId { get; set; }
}