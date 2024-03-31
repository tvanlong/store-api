using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class SubcategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public List<ProductDTO> Products { get; } = new List<ProductDTO>();
    }
}