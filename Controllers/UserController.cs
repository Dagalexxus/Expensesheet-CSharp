using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Dtos;
using api.Models;
using api.Mappers;
using api.Helpers;
using api.Data;

namespace api.Controllers{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly ApplicationDBContext _context;
        public UserController(ApplicationDBContext context){
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto registerDto){
            if (!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
            User toRegister = registerDto.toUserFromRegistration();
            string hashedPassword = Passwords.Hashing(toRegister.passwordHash, out byte[] salted);
            
            toRegister.passwordHash = hashedPassword;
            toRegister.salt = Convert.ToHexString(salted);

            await _context.Users.AddAsync(toRegister);
            _context.SaveChanges();

            return Created();
        }
    }
}