using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.DTOs
{
    public class StaffDTO
    {
         public List<OrderDTO> Orders { get;} = new List<OrderDTO>();
    }
}