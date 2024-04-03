using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using storeAPI.DTOs;
using storeAPI.Interfaces;
using storeAPI.Models;

namespace storeAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAuthService _authService;
        public CustomerController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            // var customer = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());
            // if(customer == null)
            // {
            //     return Unauthorized("Invalid username or password");
            // }

            // var result = await _signInManager.CheckPasswordSignInAsync(customer, loginDto.Password, false);
            // if(!result.Succeeded)
            // {
            //     return Unauthorized("Invalid password");
            // }

            // return Ok(new LoginResponseDTO
            // {
            //     UserName = customer.UserName,
            //     Email = customer.Email,
            //     Token = await _tokenService.CreateToken(customer),
            //     Role = (await _userManager.GetRolesAsync(customer)).FirstOrDefault()
            // });

            var response = await _authService.Login(loginDto);
            if(response == null)
            {
                return Unauthorized("Invalid username or password");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
        //    try {
        //     if(!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var customer = new Customer
        //     {
        //         UserName = registerDto.UserName,
        //         Email = registerDto.Email
        //     };

        //     var createdUser = await _userManager.CreateAsync(customer, registerDto.Password);

        //     if(createdUser.Succeeded)
        //     {
        //         var roleResult = await _userManager.AddToRoleAsync(customer, "Customer");
        //         if(roleResult.Succeeded)
        //         {
        //             return Ok(new RegisterResponseDTO
        //             {
        //                 UserName = customer.UserName,
        //                 Email = customer.Email,
        //                 Token = await _tokenService.CreateToken(customer),
        //             });
        //         }
        //         else
        //         {
        //             return StatusCode(500, roleResult.Errors);
        //         }
        //     } else {
        //         return StatusCode(500, createdUser.Errors);
        //     }

        //    } catch (Exception ex) {
        //        return StatusCode(500, ex);
        //    }
            
                var response = await _authService.Register(registerDto, "Customer");
                if(response == null)
                {
                    return StatusCode(500, "Register failed");
                }
                return Ok(response);
        }
    }
}