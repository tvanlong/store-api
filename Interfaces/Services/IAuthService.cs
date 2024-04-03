using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storeAPI.DTOs;

namespace storeAPI.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginDTO loginDto);
        Task<RegisterResponseDTO> Register(RegisterDTO registerDto, string role);
    }
}