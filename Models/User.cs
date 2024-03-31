using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace storeAPI.Models
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}