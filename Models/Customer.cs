using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class Customer : User
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}