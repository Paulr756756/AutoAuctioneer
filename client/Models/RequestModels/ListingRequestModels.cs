namespace Client.Models.RequestModels;

public class DeleteListingRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}