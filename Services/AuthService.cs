using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using storeAPI.DTOs;
using storeAPI.Interfaces;
using storeAPI.Models;

namespace storeAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public AuthService(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<LoginResponseDTO> Login(LoginDTO loginDto)
        {
            var customer = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());
            if(customer == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(customer, loginDto.Password, false);
            if(!result.Succeeded)
            {
                return null;
            }

            return new LoginResponseDTO
            {
                UserName = customer.UserName,
                Email = customer.Email,
                Token = await _tokenService.CreateToken(customer),
                Role = (await _userManager.GetRolesAsync(customer)).FirstOrDefault()
            };
        }

        public async Task<RegisterResponseDTO> Register(RegisterDTO registerDto)
        {
            var customer = new Customer
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(customer, registerDto.Password);
            if(!result.Succeeded)
            {
                return null;
            }
            return new RegisterResponseDTO
            {
                UserName = customer.UserName,
                Email = customer.Email,
                Token = await _tokenService.CreateToken(customer),
            };
        }
    }
}