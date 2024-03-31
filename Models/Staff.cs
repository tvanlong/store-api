using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Models
{
    public class Staff : User
    {
        public List<Order> Orders { get; } = new List<Order>();
    }
}