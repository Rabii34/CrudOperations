using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment_1.Models;
using Assignment_1.Data;
using Assignment_1.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ProductContext _context;
        public LoginController(ProductContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login(LoginDTO userdata)
        {
            if (userdata == null)
            {
                return BadRequest();
            }
            else
            {
                var check = _context.Retailers.FirstOrDefault(r => r.Email == userdata.Email);
                if (check != null)
                {
                    var hasher = new PasswordHasher<string>();
                    var result = hasher.VerifyHashedPassword(userdata.Email, check.Password, userdata.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        return Ok("Login successful");
                    }
                    else
                    {
                        return Unauthorized("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
