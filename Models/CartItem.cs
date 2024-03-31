using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
        public int? VersionId { get; set; }
        public Version? Version { get; set; }
    }
}