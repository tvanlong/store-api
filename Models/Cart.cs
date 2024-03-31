using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}