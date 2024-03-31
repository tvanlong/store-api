using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Images { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
        public List<Version> Versions { get; } = new List<Version>();
    }
}