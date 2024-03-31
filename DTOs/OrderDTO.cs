using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}