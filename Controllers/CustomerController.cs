using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using storeAPI.DTOs;
using storeAPI.DTOs.AuthDTOs;
using storeAPI.Interfaces;
using storeAPI.Interfaces.Repository;
using storeAPI.Models;

namespace storeAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public CustomerController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userRepository.GetAllUsers("Customer");
            if(response == null)
            {
                return NotFound("No users found");
            }
            var users = response.Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                FullName = u.FullName,
                Email = u.Email,
                Address = u.Address,
                PhoneNumber = u.PhoneNumber
            });
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userRepository.GetUserById(id);
            if(response == null)
            {
                return NotFound("User not found");
            }
            var user = new UserDTO
            {
                Id = response.Id,
                UserName = response.UserName,
                FullName = response.FullName,
                Email = response.Email,
                Address = response.Address,
                PhoneNumber = response.PhoneNumber
            };
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _authService.Login(loginDto);
            if(response == null)
            {
                return Unauthorized("Invalid username or password");
            }
            if(response.Role != "Customer")
            {
                return Unauthorized("Invalid account type");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
                var response = await _authService.Register(registerDto, "Customer");
                if(response == null)
                {
                    return StatusCode(500, "Register failed");
                }
                return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] RequestUserDTO userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                Email = userDto.Email,
                Address = userDto.Address,
                PhoneNumber = userDto.PhoneNumber,
                PasswordHash = userDto.Password
            };
            var response = await _userRepository.UpdateUser(id, user);
            if(response == null)
            {
                return NotFound("User not found");
            }
            var userResponse = new UserDTO
            {
                Id = response.Id,
                UserName = response.UserName,
                FullName = response.FullName,
                Email = response.Email,
                Address = response.Address,
                PhoneNumber = response.PhoneNumber
            };
            return Ok(userResponse);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userRepository.DeleteUser(id);
            if(response == null)
            {
                return NotFound("User not found");
            }
            return NoContent();
        }
    }
}