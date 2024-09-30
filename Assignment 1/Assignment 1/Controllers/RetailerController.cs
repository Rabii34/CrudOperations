using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment_1.Models;
using Assignment_1.Data;
using Assignment_1.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerController : ControllerBase
    {
        private readonly ProductContext _context;
        public RetailerController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Retailerss()
        {
            return Ok(_context.Retailers.ToList());
        }

        [HttpPost]
        public IActionResult Signup(RetailerDTO retailerdata)
        {
            var check = _context.Retailers.FirstOrDefault(r => r.Email == retailerdata.Email);
            if (check != null)
            {
                return BadRequest("Retailer already exists");
            }
            else
            {
                var hasher = new PasswordHasher<string>();
                var hashPass = hasher.HashPassword(retailerdata.Email, retailerdata.Password);
                //Object mapping
                // mapping domain model from DTO model
                Retailer newuser = new Retailer()
                {
                    Username = retailerdata.Retname,
                    Email = retailerdata.Email,
                    Password = hashPass,
                    RoleId = retailerdata.RoleId,
                };
                var addedRetailer = _context.Retailers.Add(newuser);
                _context.SaveChanges();
                return Ok(addedRetailer.Entity);
            }
        }
    }
}
