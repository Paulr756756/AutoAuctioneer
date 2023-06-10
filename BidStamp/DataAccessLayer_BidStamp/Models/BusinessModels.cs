using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary_BidStamp.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set;}
        public string Phone { get; set;}
        public DateOnly RegistrationDate { get; set;}

    }

    public class Stamp
    {
        public Guid StampId { get; set; }
        public string StampTitle { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Year { get; set; }
        public string? Country { get; set; }
        public string? Condition { get; set; }
        public string? CatalogNumber { get; set; }
        public int? StartingBid { get; set; }
        public int? EndingBid { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class Bid
    {
        public int BidId { get; set; }
        public int StampId { get; set; }
        public int UserId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
    }

    public class WatchList
    {
        public int WatchlistId { get; set; }
        public int UserId { get; set; }
        public int StampId { get; set; }
    }
}
