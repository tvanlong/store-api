using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storeAPI.Models;

namespace storeAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}