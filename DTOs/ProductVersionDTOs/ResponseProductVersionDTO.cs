using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs.ProductVersionDTOs
{
    public class ResponseProductVersionDTO
    {
        public int Id { get; set; }
        public string? Images { get; set; }
        public string? ProductName { get; set; }
        public string? VersionName { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }
        public string? Status { get; set; }
        public string? Details { get; set; }
        public int SubcategoryId { get; set; }
        public string? SubcategoryName { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}