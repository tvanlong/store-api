using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Images { get; set; }
        public int SubcategoryId { get; set; }  
        public List<VersionDTO> Versions { get; } = new List<VersionDTO>();
    }
}