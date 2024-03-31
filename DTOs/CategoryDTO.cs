using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<SubcategoryDTO> Subcategories { get; } = new List<SubcategoryDTO>();
    }
}