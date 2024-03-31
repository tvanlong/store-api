using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class CreateVersionDTO
    {
        public int ProductId { get; set; }
        public string VersionName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}