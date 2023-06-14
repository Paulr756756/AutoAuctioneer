namespace API_BidStamp.Models.BidRequestModels
{
    public class AddBidRequest
    {
        public Guid StampId { get; set; }
        public Guid UserId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
    }
}
