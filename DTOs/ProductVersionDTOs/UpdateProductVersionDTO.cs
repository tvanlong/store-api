using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs.ProductVersionDTOs
{
    public class UpdateProductVersionDTO
    {
        public int ProductId { get; set; }
        public string? ProductImages { get; set; }
        public int SubcategoryId { get; set; }
        public string? VersionName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }
        public string? Status { get; set; }
        public string? Details { get; set; }
    }
}
