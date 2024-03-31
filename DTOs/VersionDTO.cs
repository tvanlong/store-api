using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class VersionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProductId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public List<OrderDetailDTO> OrderDetails { get; } = new List<OrderDetailDTO>();
    }
}