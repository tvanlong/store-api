using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class OrderDetailDTO
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int VersionId { get; set; }
        public int OrderId { get; set; }
    }
}