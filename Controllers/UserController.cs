using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Dtos;
using api.Models;
using api.Mappers;
using api.Helpers;
using api.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers{

    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDBContext _context;
        public UserController(ApplicationDBContext context, IConfiguration configuration){
            _context = context;
            _configuration = configuration;
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUser){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            User? toLogin = await _context.Users.FirstOrDefaultAsync(u => u.username == loginUser.username);

            if (toLogin == null){
                return NoContent();
            }

            if (!Passwords.passwordCheck(loginUser.passwordHash, toLogin.passwordHash, Convert.FromHexString(toLogin.salt))){
                return Unauthorized();
            }
            
            string webToken = Passwords.createToken(toLogin, _configuration);

            return Ok(webToken);
        }
    }
}