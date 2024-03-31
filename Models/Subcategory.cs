using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Product> Products { get; } = new List<Product>();
    }
}