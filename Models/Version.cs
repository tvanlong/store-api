using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class Version
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product? Product { get; set; } 
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }

        public string Status { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public int? CartItemId { get; set; }
        public CartItem? CartItem { get; set; }
        public List<Order> Orders { get; } = [];
        public List<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
    }
}