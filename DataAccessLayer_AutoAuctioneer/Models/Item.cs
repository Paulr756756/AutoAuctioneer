using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer_AutoAuctioneer.Models;
public class Item {
    public int Id { get; set; } 
    public int UserId { get; set; }
    public int Type { get;set; }
}