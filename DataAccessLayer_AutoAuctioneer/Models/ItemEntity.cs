using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer_AutoAuctioneer.Models;
public class ItemEntity {
    public Guid? Id { get; set; } 
    public Guid UserId { get; set; }
    public int Type { get;set; }
}