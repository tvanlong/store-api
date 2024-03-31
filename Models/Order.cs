using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public int? CustomerId {get; set;}
        public Customer? Customer {get; set;}
        public int? StaffId {get; set;}
        public Staff? Staff {get; set;}
        public List<Version> Versions { get; set; } = [];
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}