using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class OrderDetail
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? VersionId { get; set; }
        public Version? Version { get; set; } = null!;
        public int? OrderId { get; set; }
        public Order? Order { get; set; } = null!;
    }
}